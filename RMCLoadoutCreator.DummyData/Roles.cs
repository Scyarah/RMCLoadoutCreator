namespace RMCLoadoutCreator.DummyData
{
    public static class Roles
    {

        public static readonly RMCLoadoutCreator.Definitions.Models.Role SquadLeader = new RMCLoadoutCreator.Definitions.Models.Role
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Name = "Squad Leader",
            Created = DateTimeOffset.UtcNow,
            Modified = DateTimeOffset.UtcNow,
        };

        public static readonly RMCLoadoutCreator.Definitions.Models.Role[] All = [SquadLeader];
    }
}