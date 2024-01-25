using Microsoft.AspNetCore.Identity;

namespace BEExam3.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Linkedin { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
