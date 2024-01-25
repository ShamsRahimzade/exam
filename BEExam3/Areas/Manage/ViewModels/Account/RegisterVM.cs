using System.ComponentModel.DataAnnotations;

namespace BEExam3.Areas.Manage.ViewModels.Account
{
    public class RegisterVM
    {
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Surname { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(6)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(40)]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [MaxLength(40)]
        [MinLength(8)]
        public string ConfirmPassword { get; set; }
    }
}
