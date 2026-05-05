using System.ComponentModel.DataAnnotations;

namespace RMCLoadoutCreator.Definitions.Models
{
    /// <summary>
    ///  
    /// </summary>
    public class Role : BaseEntity
    {
        /// <summary>
        /// User visible name of the role
        /// </summary>
        [Required]
        public string Name { get; set; } = "";
    }
}