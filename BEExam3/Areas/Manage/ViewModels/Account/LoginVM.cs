using System.ComponentModel.DataAnnotations;

namespace BEExam3.Areas.Manage.ViewModels.Account
{
    public class LoginVM
    {
        [Required]
        
        public string EmailOrUsername { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(40)]
        [MinLength(8)]
        public string Password { get; set; }
        public bool IsRemembered { get; set; }
    }
}
