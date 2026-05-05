namespace RMCLoadoutCreator.Definitions.Models
{
    public class Item : BaseEntity
    {
        /// <summary>
        /// Prototype ID of the item
        /// </summary>
        public string? EntityId { get; set; }

        /// <summary>
        /// ID of the slot this item is contained in
        /// </summary>
        public Guid SlotId { get; set; }

        /// <summary>
        /// The slot this item is contained in
        /// </summary>
        public virtual Slot Slot { get; set; } = null!;

        /// <summary>
        /// ID of the parent item if this item is contained in another item
        /// </summary>
        public virtual Guid ParentId { get; set; }

        /// <summary>
        /// Parent of the item if it is contained in another item
        /// </summary>
        public virtual Item? Parent { get; set; }
    }
}