using AdminPanel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PanelContext _context;
        public HomeController(ILogger<HomeController> logger, PanelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> AddSort(Sort sort)
        {
            await _context.AddAsync(sort);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Sort));
        }
        public IActionResult Index()
        {            
            var list = _context.Pages.Where(x=> x.IsPublish).Take(4).OrderByDescending(x => x.PageId).ToList();            
            ViewBag.PageId = _context.Pages.ToList();
            return View(list);     
        }

        public IActionResult GetPartial() => new PartialViewResult
        {
            ViewName = "../Home/_NavbarPartial",
            ViewData = ViewData,           

        };
        public IActionResult _NavbarPartial()
        {
            var list = _context.Pages.Where(x => x.IsPublish).OrderByDescending(x => x.IdofSort).ToList();
            ViewBag.PageId = _context.Pages.ToList();
            return View(list);
        }

        public IActionResult About()
        {
            int Id = 5;
            var page = _context.Pages.Find(Id);
            return View(page);
        }
        
        public IActionResult Solutions()
        {
            int Id = 4;
            var page = _context.Pages.Find(Id);
            return View(page);
        }
        public IActionResult Contact()
        {
            int Id = 3;
            var page = _context.Pages.Find(Id);
            return View(page);

        }
        public IActionResult Page(int? Id)
        {
            if (Id != null)
            {
                Page page = _context.Pages.Find(Id);
                return View(page);
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
