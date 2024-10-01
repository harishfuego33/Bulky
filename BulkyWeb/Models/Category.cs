using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BulkyBookWeb.Models
{
    // DB schema
    public class Category
    {
        [Key]// constraints for database for eg: Primary Key
        public int Id { set; get;}
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(30)]
        public string Name {set; get;}
        [DisplayName("Display Order")]
        [Range(1, 100)]
        public int DisplayOrder { set; get; }
    }
}