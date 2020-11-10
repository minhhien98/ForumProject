using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumProject.Models.ViewModel.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "User name must have 6 characters or above!!!")]
        [Display(Name ="Username:")]
        public string UserName { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password must have 6 characters or above!!!")]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "First name:")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name:")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Role:")]
        public int UserRoleId { get; set; }       
        public List<SelectListItem> UserRole { get; set; }
        public byte[] AvatarImage { get; set; }
    }
}
