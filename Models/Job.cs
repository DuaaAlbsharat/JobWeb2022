using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobWeb2022.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }
        [Required]
        [Display(Name ="إسم الوظيفة")]
        public string JobTile { get; set; }
        [Required]
        [Display(Name = "وصف الوظيفة")]
        public string JobContent { get; set; }
        //[Required(ErrorMessage ="الرجاء تحميل صورة للوظيفة")]
        
        [Display(Name = "صورة الوظيفة")]
        public string JobImage { get; set; }
        [Display(Name = "نوع الوظيفة")]
        public int CategoryId { get; set; }
        public string UserId { get; set; }
        public  virtual ApplicationUser User { get; set; }
        public virtual Category Category { get; set; }
    }
}