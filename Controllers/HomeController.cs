using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using manualdojovem.Data;
using manualdojovem.Models;
using manualdojovem.ViewModels;


namespace manualdojovem.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Contexto _context;
        public HomeController(ILogger<HomeController> logger, Contexto contexto)
        {
            _logger = logger;
            _context = contexto;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Article(int id)
        {
            var articleVM = new ArticleVM()
            {
                Category = _context.Categories.Find(id),
                Articles = _context.Articles.Where(a => a.CategoryId == id).ToList()
            };
            return View(articleVM);
        }

        public IActionResult Service(int id)
        {
            var service = new CategoryVM()
            {
                Articles = _context.Articles.Include(a => a.Category).ToList(),
                Categories = _context.Categories.ToList()
            };
            return View(service);
        }

        public IActionResult Read(int id)
        {
            var readVM = new ReadVM()
            {
                Article = _context.Articles.Find(id) ,
                Category = _context.Categories.Find(id)
            };
            return View(readVM);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
