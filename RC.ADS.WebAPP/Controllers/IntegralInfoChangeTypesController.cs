using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RC.ADS.Data;
using RC.ADS.Data.Entity.AD_Integral;

namespace RC.ADS.WebAPP.Controllers
{
    public class IntegralInfoChangeTypesController : Controller
    {
        private readonly DataContext _context;

        public IntegralInfoChangeTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: IntegralInfoChangeTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.IntegralInfoChangeType.ToListAsync());
        }

        // GET: IntegralInfoChangeTypes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var integralInfoChangeType = await _context.IntegralInfoChangeType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (integralInfoChangeType == null)
            {
                return NotFound();
            }

            return View(integralInfoChangeType);
        }

        // GET: IntegralInfoChangeTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IntegralInfoChangeTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Describe")] IntegralInfoChangeType integralInfoChangeType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(integralInfoChangeType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(integralInfoChangeType);
        }

        // GET: IntegralInfoChangeTypes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var integralInfoChangeType = await _context.IntegralInfoChangeType.FindAsync(id);
            if (integralInfoChangeType == null)
            {
                return NotFound();
            }
            return View(integralInfoChangeType);
        }

        // POST: IntegralInfoChangeTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Describe")] IntegralInfoChangeType integralInfoChangeType)
        {
            if (id != integralInfoChangeType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(integralInfoChangeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntegralInfoChangeTypeExists(integralInfoChangeType.Id))
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
            return View(integralInfoChangeType);
        }

        // GET: IntegralInfoChangeTypes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var integralInfoChangeType = await _context.IntegralInfoChangeType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (integralInfoChangeType == null)
            {
                return NotFound();
            }

            return View(integralInfoChangeType);
        }

        // POST: IntegralInfoChangeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var integralInfoChangeType = await _context.IntegralInfoChangeType.FindAsync(id);
            _context.IntegralInfoChangeType.Remove(integralInfoChangeType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntegralInfoChangeTypeExists(string id)
        {
            return _context.IntegralInfoChangeType.Any(e => e.Id == id);
        }
    }
}
