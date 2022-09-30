using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BookShop.UI.Models.ViewModel
{
    public class CoverTypeVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Cover Type")]
        [MaxLength(50)]
        public string Name { get; set; }
       
      
      
    }
}
