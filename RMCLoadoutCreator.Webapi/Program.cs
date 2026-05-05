using RMCLoadoutCreator.WebApi.Components;
using RMCLoadoutCreator.Definitions;
using RMCLoadoutCreator.DummyData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<LoadoutCreatorContext>(provider =>
    LoadoutCreatorContextFactory.Create(builder.Configuration.GetConnectionString("DefaultConnection")!));

// Configure Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
    // Configure password requirements
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    
    // Configure lockout
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    
    // Configure user settings
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<LoadoutCreatorContext>()
.AddDefaultTokenProviders();

// Configure OAuth authentication (if enabled)
var authBuilder = builder.Services.AddAuthentication();
if (builder.Configuration.GetValue<bool>("OAuth:Enabled", false))
{
    authBuilder.AddOAuth("CustomProvider", options => {
        options.ClientId = builder.Configuration["OAuth:ClientId"] ?? "";
        options.ClientSecret = builder.Configuration["OAuth:ClientSecret"] ?? "";
        options.AuthorizationEndpoint = builder.Configuration["OAuth:AuthorizationEndpoint"] ?? "https://example.com/oauth/authorize";
        options.TokenEndpoint = builder.Configuration["OAuth:TokenEndpoint"] ?? "https://example.com/oauth/token";
        options.UserInformationEndpoint = builder.Configuration["OAuth:UserInformationEndpoint"] ?? "https://example.com/api/user";
        
        // Configure scopes
        options.Scope.Add("email");
        options.Scope.Add("profile");
        
        options.Events.OnCreatingTicket = async context =>
        {
            // Get user info from OAuth provider
            var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.AccessToken);
            
            var response = await context.Backchannel.SendAsync(request);
            var user = System.Text.Json.JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            
            // Simple claim mapping - to be customized based on your OAuth provider's response
            var userId = user.RootElement.GetProperty("id").GetString();
            var email = user.RootElement.GetProperty("email").GetString();
            var name = user.RootElement.GetProperty("name").GetString();
            
            if (!string.IsNullOrEmpty(userId))
                context.Identity?.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
            if (!string.IsNullOrEmpty(email))
                context.Identity?.AddClaim(new Claim(ClaimTypes.Email, email));
            if (!string.IsNullOrEmpty(name))
                context.Identity?.AddClaim(new Claim(ClaimTypes.Name, name));
        };
    });
}

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add authentication state for Blazor
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

// Run Entity Framework migrations on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LoadoutCreatorContext>();
    context.Database.Migrate();
    
    // Seed dummy data if configured
    if (builder.Configuration.GetValue<bool>("DummyData:SeedData", false))
    {
        DummyDataSeeder.Seed(context);
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapControllers();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
