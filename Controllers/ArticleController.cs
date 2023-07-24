using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using manualdojovem.Models;
using manualdojovem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace manualdojovem.Controllers
{
    public class ArticleController : Controller
    {
        private readonly Contexto _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ArticleController(Contexto context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Article
        public IActionResult Index()
        {
            return View();
        }

         [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var articles = await _context.Articles.Include(p => p.Category).ToListAsync();
            return Json(new { data = articles } );
        }

        // GET: Article/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Article/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Article/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CategoryId,Image")] Article article, IFormFile file)
        {
            if (ModelState.IsValid)
            { 
                if (file != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString();
                    string uploads = Path.Combine(wwwRootPath, @"img\articles");
                    string extension = Path.GetExtension(file.FileName);
                    string newFile = Path.Combine(uploads, fileName + extension);
                    using (var stream = new FileStream(newFile, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    article.Image = @"\img\articles\" + fileName + extension;
                }
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", article.CategoryId);
            return View(article);
        }

        // GET: Article/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", article.CategoryId);
            return View(article);
        }

        // POST: Article/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CategoryId,Image")] Article article, IFormFile file)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString();
                    string uploads = Path.Combine(wwwRootPath, @"img\articles");
                    string extension = Path.GetExtension(file.FileName);
                    
                    if (article.Image != null)
                    {
                        string oldFile = Path.Combine(wwwRootPath, article.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }
                    }
                    
                    string newFile = Path.Combine(uploads, fileName + extension);
                    using (var stream = new FileStream(newFile, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    article.Image = @"\img\articles\" + fileName + extension;
                }
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", article.CategoryId);
            return View(article);
        }

        // GET: Article/Delete/5
      [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var article = _context.Articles.Find(id);
            if (article == null)
            {
                return Json(new { success = false, message = "Artigo não encontrado"});
            }
            try
            {
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return Json(new { success = false, message = "Informe ao Suporte Técnico"});
            }
            return Json(new { success = true, message = "Excluído com Sucesso"});
        }
        
        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
