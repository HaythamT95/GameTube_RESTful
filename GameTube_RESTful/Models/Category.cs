using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameTube_RESTful.Models
{
    public class Category
    {
        [Key] //Primary key
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        // Navigation property for the many-to-many relationship
        public ICollection<GameCategory> GameCategories { get; set; }
    }
}
