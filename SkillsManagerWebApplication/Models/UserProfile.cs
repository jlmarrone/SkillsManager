using SkillsManagerWebApplication.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SkillsManagerWebApplication.Models
{
    [Table("UserProfile")]
    public class UserProfile
    {
        private SkillDBContext db = new SkillDBContext();

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [RegularExpression("^[0-9]{8}$", ErrorMessage = "Please enter a proper DNI number.")]
        public string Dni { get; set; }

        [Display(Name = "Remember on this computer")]
        public bool RememberMe { get; set; }
        public bool IsValid(string _username, string _password)
        {
            string password = Helpers.SHA1.Encode(_password);

            var users = from r in db.UserProfiles
                        where r.UserName == _username && r.Password == password
                        select r;

            if (users.Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public virtual ICollection<Skill> Skills { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("SkillDBContext")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}