using Microsoft.AspNetCore.Identity;

namespace BEExam3.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
