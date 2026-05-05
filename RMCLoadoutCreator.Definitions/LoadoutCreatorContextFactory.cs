using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RMCLoadoutCreator.Definitions
{
    public class LoadoutCreatorContextFactory : IDesignTimeDbContextFactory<LoadoutCreatorContext>
    {
        public LoadoutCreatorContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LoadoutCreatorContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=mydb;Username=postgres;Password=Qw123456");

            return new LoadoutCreatorContext(optionsBuilder.Options);
        }

        public static LoadoutCreatorContext Create(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LoadoutCreatorContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new LoadoutCreatorContext(optionsBuilder.Options);
        }
    }
}