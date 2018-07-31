using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RC.ADS.Data;
using RC.ADS.Data.Entity.AD_Article;
using RC.ADS.WebAPP.Comm;

namespace RC.ADS.WebAPP.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly DataContext _context;
        private IHostingEnvironment _env;
        

        public ArticlesController(DataContext context,IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Articles.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }
        [HttpGet]
        // GET: Articles/Create
        public IActionResult Create()
        {
            //var data = from p in db.PersonalDetails
            //           join f in db.Files
            //           on p.AutoId equals f.PersonalDetailsId
            //           select new
            //           {
            //               PersonName = p.FirstName,
            //               MyFileName = f.FileName
            //           };

            //SelectList list = new SelectList(data, "MyFileName", "PersonName");
            //ViewBag.Roles = list;
            var selectListEnum = _context.ArticleTypes.Select(x => new { Value = x.Id, Text = x.Name });
            SelectList list = new SelectList(selectListEnum, "Value", "Text");
            ViewBag.SelectListEnum = list;
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Article article)
        {

            if (ModelState.IsValid)
            {
               

                var articleIco_File = Request.Form.Files["ArticleIco"] ;
                 article.ArticleIco= FileHelper.UploadImage(articleIco_File, _env);

                var articleImage_File = Request.Form.Files["ArticleImage"];
                article.ArticleImage = FileHelper.UploadImage(articleImage_File, _env);


                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(string id)
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
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ArticleName,ArticleContent,ArticleIco,ArticleImage,ArticleIndex")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var old_article = await _context.Articles.FindAsync(id);
                    old_article.ArticleContent = article.ArticleContent;
                    old_article.ArticleIndex = article.ArticleIndex;
                   // old_article.ArticleTypeEntity =await _context.ArticleTypes.FindAsync(article.ArticleTypeEntity.Id);

                    var articleIco_File = Request.Form.Files["ArticleIco"];
                    if (articleIco_File!=null)
                    {
                        article.ArticleIco = FileHelper.UploadImage(articleIco_File, _env);
                    }
                    else
                    {
                        article.ArticleIco= old_article.ArticleIco;
                    }
                   

                    var articleImage_File = Request.Form.Files["ArticleImage"];
                    if (articleImage_File != null)
                    {
                        article.ArticleImage = FileHelper.UploadImage(articleImage_File, _env);
                    }
                    else
                    {
                        article.ArticleImage = old_article.ArticleImage;
                    }



                    _context.Update(old_article);
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
            return View(article);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var article = await _context.Articles.FindAsync(id);
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(string id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
