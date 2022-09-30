using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BookShop.UI.Models.ViewModel
{
    public class CategoryVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, 100, ErrorMessage = "Rang Should be 1 to 100")]
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now;
    }
}
