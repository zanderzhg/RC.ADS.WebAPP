using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RC.ADS.Data;
using RC.ADS.Data.Entity.AD_Article;

namespace RC.ADS.WebAPP.Controllers
{
    public class ArticleTypesController : Controller
    {
        private readonly DataContext _context;

        public ArticleTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: ArticleTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ArticleTypes.ToListAsync());
        }

        // GET: ArticleTypes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleType = await _context.ArticleTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articleType == null)
            {
                return NotFound();
            }

            return View(articleType);
        }

        // GET: ArticleTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ArticleTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Describe")] ArticleType articleType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articleType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(articleType);
        }

        // GET: ArticleTypes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleType = await _context.ArticleTypes.FindAsync(id);
            if (articleType == null)
            {
                return NotFound();
            }
            return View(articleType);
        }

        // POST: ArticleTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Describe")] ArticleType articleType)
        {
            if (id != articleType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articleType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleTypeExists(articleType.Id))
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
            return View(articleType);
        }

        // GET: ArticleTypes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleType = await _context.ArticleTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articleType == null)
            {
                return NotFound();
            }

            return View(articleType);
        }

        // POST: ArticleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var articleType = await _context.ArticleTypes.FindAsync(id);
            _context.ArticleTypes.Remove(articleType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleTypeExists(string id)
        {
            return _context.ArticleTypes.Any(e => e.Id == id);
        }
    }
}
