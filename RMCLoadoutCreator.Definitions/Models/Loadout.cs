namespace RMCLoadoutCreator.Definitions.Models
{
    public class Loadout : BaseEntity
    {
        /// <summary>
        /// Notes about the loadout, visible to users. Optional.
        /// </summary>
        public string? Notes { get; set; }
        
        /// <summary>
        /// Foreign key to the role this loadout belongs to
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Navigation property to the role this loadout belongs to
        /// </summary>
        public virtual Role Role { get; set; } = null!;

        /// <summary>
        /// Foreign key to the version this loadout belongs to
        /// </summary>
        public Guid VersionId { get; set; }

        /// <summary>
        /// Navigation property to the version this loadout belongs to
        /// </summary>
        public virtual Version Version { get; set; } = null!;

        /// <summary>
        /// Collection of slots in this loadout
        /// </summary>
        public virtual ICollection<Slot> Slots { get; set; } = null!;
    }
}