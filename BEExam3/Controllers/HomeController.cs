using BEExam3.DAL;
using BEExam3.Models;
using BEExam3.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BEExam3.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
           _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await _context.Employees.Include(e => e.Position).ToListAsync();
            HomeVM vm = new HomeVM
            {
                Employees = employees
            };
            return View(vm);
        }

       
    }
}