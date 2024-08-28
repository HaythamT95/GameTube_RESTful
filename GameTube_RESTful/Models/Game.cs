using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace GameTube_RESTful.Models
{
    public class Game
    {
        [Key] // Primary key attribute
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameId { get; set; }

        [Required]
        public string GameName { get; set; }

        [Required]
        public ICollection<GameCategory> GameCategories { get; set; }

    }
}
 