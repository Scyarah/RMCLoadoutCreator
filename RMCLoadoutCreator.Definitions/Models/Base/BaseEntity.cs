namespace RMCLoadoutCreator.Definitions.Models
{
    public class BaseEntity
    {
        /// <summary>
        /// Primary key for the entity
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Timestamp of creation date
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Timestamp of last modification date
        /// </summary>
        public DateTimeOffset Modified { get; set; }
    }
}