using System.ComponentModel.DataAnnotations;

namespace TheGameChanger.Models
{
    public class UpdateNameDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        [Required]
        [MaxLength(25)]
        public string NewName { get; set; }
    }
}
