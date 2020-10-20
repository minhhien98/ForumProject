using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumProject.Models.ViewModel.Thread
{
    public class ThreadViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Thread Name:")]
        public string ThreadName { get; set; }
        [Required]
        [Display(Name ="Section:")]
        public int SectionId { get; set; }
        public List<SelectListItem> Section { get; set; }

    }
}
