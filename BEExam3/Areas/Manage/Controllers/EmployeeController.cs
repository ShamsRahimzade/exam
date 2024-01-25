using BEExam3.Areas.Manage.ViewModels.Employee;
using BEExam3.DAL;
using BEExam3.Models;
using BEExam3.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BEExam3.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await _context.Employees.Include(e => e.Position).ToListAsync();
            return View(employees);
        }
        public async Task<IActionResult> Create()
        {
            List<Position> positions = await _context.Positions.ToListAsync();
            CreateEmployeeVM vm = new CreateEmployeeVM
            {
                Positions = positions,
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            vm.Positions = await _context.Positions.ToListAsync();
            Employee employee = new Employee
            {
                FullName = vm.Name,
                Twitter = vm.Twitter,
                Facebook = vm.Facebook,
                Linkedin = vm.Linkedin,
                Instagram = vm.Instagram,
                PositionId = vm.PositionId,
            };
            if (vm.Photo is not null)
            {
                if (!vm.Photo.CheckSize(7))
                {
                    ModelState.AddModelError("Photo", "wrong size");
                    return View(vm);
                }
                if (!vm.Photo.CheckType("image/"))
                {
                    ModelState.AddModelError("Photo", "wrong type");
                    return View(vm);
                }
                string filename = await vm.Photo.CreateFile(_env.WebRootPath, "assets", "img");
                employee.Image = filename;
            }
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Employee employee = await _context.Employees.Include(e => e.Position).FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) return NotFound();
            List<Position> positions = await _context.Positions.ToListAsync();
            UpdateEmployeeVM vm = new UpdateEmployeeVM
            {
                Name = employee.FullName,
                Twitter = employee.Twitter,
                Facebook = employee.Facebook,
                Linkedin = employee.Linkedin,
                Instagram = employee.Instagram,
                PositionId = employee.PositionId,
                Image = employee.Image,
                Positions = positions,
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateEmployeeVM vm)
        {
            if (id <= 0) return BadRequest();
            Employee employee = await _context.Employees.Include(e => e.Position).FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) return NotFound();
            vm.Positions = await _context.Positions.ToListAsync();
            if (vm.Photo is not null)
            {
                if (!vm.Photo.CheckSize(7))
                {
                    ModelState.AddModelError("Photo", "wrong size");
                    return View(vm);
                }
                if (!vm.Photo.CheckType("image/"))
                {
                    ModelState.AddModelError("Photo", "wrong type");
                    return View(vm);
                }
                string filename = await vm.Photo.CreateFile(_env.WebRootPath, "assets", "img");
                employee.Image.DeleteFile(_env.WebRootPath, "assets", "img");
                employee.Image = filename;
            }
            employee.FullName = vm.Name;
            employee.Facebook = vm.Facebook;
            employee.Instagram = vm.Instagram;
            employee.Linkedin = vm.Linkedin;
            employee.Twitter = vm.Twitter;
            employee.PositionId = vm.PositionId;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {

            if (id <= 0) return BadRequest();
            Employee employee = await _context.Employees.Include(e => e.Position).FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) return NotFound();
            if (employee.Image is not null)
            {

                employee.Image.DeleteFile(_env.WebRootPath, "assets", "img");
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
