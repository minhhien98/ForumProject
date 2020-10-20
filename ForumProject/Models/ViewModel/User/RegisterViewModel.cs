using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumProject.Models.ViewModel.User
{
    public class RegisterViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "User name must have 6 characters or above!!!")]
        public string UserName { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password must have 6 characters or above!!!")]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int UserRoleId = 2;
    }
}
