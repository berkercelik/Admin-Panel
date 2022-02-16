using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Models;
using Microsoft.AspNetCore.Http;
using AdminPanel.Filter;
using Microsoft.Extensions.Logging;


namespace AdminPanel.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly PanelContext _context;
        public AdminController(ILogger<AdminController> logger, PanelContext context)
        {
            _logger = logger;
            _context = context;
            
        }
        public IActionResult Index()
        {
            return View();
        }

        [UserFilter]
        public IActionResult Sort()
        {
            List<Sort> list = _context.Sorts.ToList();
            return View(list);
        }
        [UserFilter]
        public IActionResult Admin()
        {
            List<Admin> list = _context.Admins.ToList();
            return View(list);
        }

        #region Sort
        //-----------------------------------sort
        public async Task<IActionResult> AddSort(Sort sort)
        {
            if (sort.SortId == 0)
            {
                await _context.AddAsync(sort);
                
            }
            else
            {
                _context.Update(sort);
            }
            
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Sort));
        }
        
        public async Task<IActionResult> DeleteSort(int? id)
        {
            Sort sort = await _context.Sorts.FindAsync(id);
            _context.Remove(sort);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Sort));
        }
        
            
        public async Task<IActionResult> SortDetails(int SortId)
        {
            var sort = await _context.Sorts.FindAsync(SortId);
            return Json(sort);
        }
        #endregion
        #region Admin
        //-------------------------------------admin
        public async Task<IActionResult> AddAdmin(Admin admin)
        {
            if (admin.Id == 0)
            {
                await _context.AddAsync(admin);

            }
            else
            {
                _context.Update(admin);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Admin));

        }
        public async Task<IActionResult> DeleteAdmin(int? id)
        {
            Admin admin = await _context.Admins.FindAsync(id);
            _context.Remove(admin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(admin));
        }

        public async Task<IActionResult> AdminDetails(int Id)
        {
            var admin = await _context.Admins.FindAsync(Id);
            return Json(admin);
        }
        #endregion
        #region Login & Logout
        //-------------------------------------login
        public async Task<IActionResult> Login(string Email,string Password)
        {
            var admin = _context.Admins.FirstOrDefault(w => w.Email == Email && w.Password == Password);
            if (admin == null)
            {
                return RedirectToAction(nameof(Index));
            }
            
            HttpContext.Session.SetInt32("id", admin.Id);
            
            return RedirectToAction(nameof(Index));
        }
        //-------------------------------------logout
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index));
        }
        #endregion


    }
}
