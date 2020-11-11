using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumProject.Models.ViewModel.User
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage ="Please enter your password")]
        [Display(Name ="Old Password:")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage ="Please enter new password")]
        [MinLength(6,ErrorMessage ="Password must be above 6 characters")]
        [Display(Name ="New Password:")]
        public string NewPassword { get; set; }
    }
}
