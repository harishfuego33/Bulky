using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    // DB sechema
    public class Category
    {
        [Key]// constrants for database for eg: Primary Key
        public int Id { set; get;}
        [Required]
        public string Name {set; get;}
        public int DisplayOrder { set; get; }
    }
}
