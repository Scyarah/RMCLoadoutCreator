namespace RMCLoadoutCreator.Definitions.Models
{
    public class Vendor : BaseEntity
    {
        /// <summary>
        /// Human readable name of the vendor
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Optional description of the vendor
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// ID of the version this vendor corresponds to
        /// </summary>
        public Guid VersionId { get; set; }

        /// <summary>
        /// The version this vendor corresponds to
        /// </summary>
        public virtual Version Version { get; set; } = null!;        
    }
}