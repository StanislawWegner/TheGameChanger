using System.ComponentModel.DataAnnotations;

namespace TheGameChanger.Models
{
    public class CreateTypOfGameDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
    }
}
