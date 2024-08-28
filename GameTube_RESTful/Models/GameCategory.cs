using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameTube_RESTful.Models
{
    public class GameCategory
    {
        [Key, Column(Order = 0)]
        public int GameId { get; set; }
        public Game Game { get; set; }

        [Key, Column(Order = 1)]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
