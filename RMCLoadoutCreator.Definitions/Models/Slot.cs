using RMCLoadoutCreator.Definitions.Enums;

namespace RMCLoadoutCreator.Definitions.Models
{
    public class Slot : BaseEntity
    {
        /// <summary>
        /// Type of the slot
        /// </summary>
        public SlotType Type { get; set; }

        /// <summary>
        /// ID of the loadout this slot belongs to
        /// </summary>
        public Guid LoadoutId { get; set; }

        /// <summary>
        /// The loadout this slot belongs to
        /// </summary>
        public virtual Loadout Loadout { get; set; } = null!;

        /// <summary>
        /// ID of the item contained in this slot, if any
        /// </summary>
        public Guid? ItemId { get; set; }

        /// <summary>
        /// The item contained in this slot, if any
        /// </summary>
        public virtual Item? Item { get; set; }
    }
}