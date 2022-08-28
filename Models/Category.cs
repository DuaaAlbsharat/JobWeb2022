using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobWeb2022.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [Display(Name ="نوع الوظيفه")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "وصف الوظيفه")]
        public string CategoryDescription { get; set; }
        public virtual ICollection <Job> Jobs { get; set; }
    }
}