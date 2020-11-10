using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace ForumProject.Models.ViewModel.User
{
    public class ChangeAvatarViewModel
    {
        [Required(ErrorMessage ="Please Choose the Image")]
        [Display(Name ="Image:")]
        public IFormFile AvatarImage { get; set; }
    }
}
