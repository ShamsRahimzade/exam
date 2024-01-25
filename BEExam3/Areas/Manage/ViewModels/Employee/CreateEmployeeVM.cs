using BEExam3.Models;

namespace BEExam3.Areas.Manage.ViewModels.Employee
{
    public class CreateEmployeeVM
    {
        public string Name { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string Linkedin { get; set; }
        public IFormFile Photo { get; set; }
        public List<Position>? Positions { get; set; }
        public int PositionId { get; set; }


    }
}
