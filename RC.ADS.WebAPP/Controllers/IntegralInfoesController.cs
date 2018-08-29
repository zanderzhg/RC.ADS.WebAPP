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
    public class IntegralInfoesController : Controller
    {
        private readonly DataContext _context;

        public IntegralInfoesController(DataContext context)
        {
            _context = context;
        }

        // GET: IntegralInfoes
      
        public async Task<IActionResult> Index(string owerId)
        {
            if (owerId == null)
            {
                //owerId = this.TempData["owerId"].ToString();
            }
            ViewBag.OwerName = _context.Menbers.FirstOrDefault(x => x.Id == owerId).Username;
            ViewBag.OwerId = owerId;
            return View(await _context.IntegralInfos.Where(x => x.OwnerId == owerId).ToListAsync());

        }

        // GET: IntegralInfoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var integralInfo = await _context.IntegralInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (integralInfo == null)
            {
                return NotFound();
            }

            return View(integralInfo);
        }
   
 
        // GET: IntegralInfoes/Create
        public IActionResult Create(string owerId)
        {
            ViewBag.OwerName = _context.Menbers.FirstOrDefault(x => x.Id == owerId).Username;
            IntegralInfo integralInfo = new IntegralInfo() { OwnerId = owerId };
            return View(integralInfo);
        }

        // POST: IntegralInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IntegralInfo integralInfo)
        {
            if (ModelState.IsValid)
            {
                var menber = _context.Menbers.FirstOrDefault(x => x.Id == integralInfo.OwnerId);
                menber.IntegralSum += integralInfo.Score;
                _context.Add(integralInfo);
                _context.Menbers.Update(menber);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),"Menbers");
            }
            return View(integralInfo);
        }

        // GET: IntegralInfoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var integralInfo = await _context.IntegralInfos.FindAsync(id);
            if (integralInfo == null)
            {
                return NotFound();
            }
            return View(integralInfo);
        }

        // POST: IntegralInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Score,IntegralInfoChangeType,Describe")] IntegralInfo integralInfo)
        {
            if (id != integralInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(integralInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntegralInfoExists(integralInfo.Id))
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
            return View(integralInfo);
        }

        // GET: IntegralInfoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var integralInfo = await _context.IntegralInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (integralInfo == null)
            {
                return NotFound();
            }

            return View(integralInfo);
        }

        // POST: IntegralInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var integralInfo = await _context.IntegralInfos.FindAsync(id);
            _context.IntegralInfos.Remove(integralInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntegralInfoExists(string id)
        {
            return _context.IntegralInfos.Any(e => e.Id == id);
        }
    }
}
