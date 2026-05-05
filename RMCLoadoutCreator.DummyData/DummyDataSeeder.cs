namespace RMCLoadoutCreator.DummyData
{
    public static class DummyDataSeeder
    {

        public static void Seed(RMCLoadoutCreator.Definitions.LoadoutCreatorContext context)
        {
            SeedRoles(context);
        }
        
        private static void SeedRoles(RMCLoadoutCreator.Definitions.LoadoutCreatorContext context)
        {
            
            foreach (var role in Roles.All)
            {
                if (!context.RMCRoles.Any(it => it.Id == role.Id))
                {
                    context.RMCRoles.Add(role);
                }
            }
            context.SaveChanges();
        }
    }
}