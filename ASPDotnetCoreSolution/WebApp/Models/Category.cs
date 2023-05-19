using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Category
    {
        [Key]
        // Key data annotation(attribute) sets the Id column as the primary key and identity column
        public int Id { get; set; }
        [Required]
        public int Name { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        // sets DateTime.Now as default value

    }
}
