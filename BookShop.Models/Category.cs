using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Models
{
    public class Category
    {
        public  int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1,100,ErrorMessage ="Rang Should be 1 to 100")]
        [Display(Name= "Display Order")]
        public int DisplayOrder { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now;

    }
}
