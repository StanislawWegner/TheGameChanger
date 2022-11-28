using System.ComponentModel.DataAnnotations;

namespace TheGameChanger.Models
{
    public class CreateGameDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        

    }
}
