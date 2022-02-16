using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Models;
using Microsoft.AspNetCore.Http;
using AdminPanel.Filter;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AdminPanel.Controllers
{
    [UserFilter]
    public class PageController : Controller
    {
        private readonly ILogger<PageController> _logger;
        private readonly PanelContext _context;
        
        public PageController(ILogger<PageController> logger, PanelContext context)
        {
            _context = context;
            _logger = logger;

        }
        public IActionResult Index()
        {
            List<Page> list = _context.Pages.ToList();
            return View(list);
            
        }
        public IActionResult EditPage(int? Id)
        {
            ViewBag.Sorts = _context.Sorts.Select(w =>
                new SelectListItem
                {
                    Text = w.PageName,
                    Value = w.SortId.ToString()
                }
            ).ToList();

            if (Id != null)
            {
                var getpage = _context.Pages.Find(Id);
                return View("EditPage", getpage);
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }
        #region Update
        [HttpPost]
        public async Task<IActionResult> UpdatePage(Page page)
        {            
            _context.Update(page);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var page = await _context.Pages.FindAsync(id);
            return Json(page);
        }
        #endregion

        #region Create & Delete
        public IActionResult CreatePage()
        {
            ViewBag.Sorts = _context.Sorts.Select(w =>
                new SelectListItem
                {
                    Text = w.PageName,
                    Value = w.SortId.ToString()
                }
            ).ToList();
            return View();
        }
        public async Task<IActionResult> DeletePage(int? id)
        {
            Page page = await _context.Pages.FindAsync(id);
            _context.Remove(page);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Publish & Unpublish
        public IActionResult SaveToMain(int Id)
        {
            Page page = _context.Pages.Find(Id);
            page.IsPublish=true;
            _context.Update(page);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult RemoveFromMain(int Id)
        {
            Page page = _context.Pages.Find(Id);
            page.IsPublish = false;
            _context.Update(page);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        public async Task<IActionResult> Save(Page model)
        {
            if (model != null)
            {
                await _context.AddAsync(model);
                await _context.SaveChangesAsync();
                return Json(true);
            }
            return Json(false);
        }













    }
}
