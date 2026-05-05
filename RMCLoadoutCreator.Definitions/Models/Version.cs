using System.ComponentModel.DataAnnotations;

namespace RMCLoadoutCreator.Definitions.Models
{
    public class Version : BaseEntity
    {
        /// <summary>
        /// The hash of the git commit this version corresponds to
        /// </summary>
        [Required]
        public required string Hash { get; set; }
    }
}