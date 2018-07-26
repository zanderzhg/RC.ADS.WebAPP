using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RC.ADS.Data;
using RC.ADS.Data.Entity.AD_Menber;

namespace RC.ADS.WebAPP.Controllers
{
    public class MenbersController : Controller
    {
        private readonly DataContext _context;

        public MenbersController(DataContext context)
        {
            _context = context;
        }

        // GET: Menbers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Menbers.ToListAsync());
        }

        // GET: Menbers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menber = await _context.Menbers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menber == null)
            {
                return NotFound();
            }

            return View(menber);
        }

        // GET: Menbers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Menbers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ManberName,PhoneNumber,Password,AccountSum,IntegralSum")] Menber menber)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menber);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menber);
        }

        // GET: Menbers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menber = await _context.Menbers.FindAsync(id);
            if (menber == null)
            {
                return NotFound();
            }
            return View(menber);
        }

        // POST: Menbers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ManberName,PhoneNumber,Password,AccountSum,IntegralSum")] Menber menber)
        {
            if (id != menber.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menber);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenberExists(menber.Id))
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
            return View(menber);
        }

        // GET: Menbers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menber = await _context.Menbers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menber == null)
            {
                return NotFound();
            }

            return View(menber);
        }

        // POST: Menbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var menber = await _context.Menbers.FindAsync(id);
            _context.Menbers.Remove(menber);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenberExists(string id)
        {
            return _context.Menbers.Any(e => e.Id == id);
        }
    }
}
