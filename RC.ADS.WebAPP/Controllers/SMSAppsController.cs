using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RC.ADS.Data;
using RC.ADS.Data.Entity.AD_SMS;

namespace RC.ADS.WebAPP.Controllers
{
    public class SMSAppsController : Controller
    {
        private readonly DataContext _context;

        public SMSAppsController(DataContext context)
        {
            _context = context;
        }

        // GET: SMSApps
        public async Task<IActionResult> Index()
        {
            return View(await _context.SMSApp.ToListAsync());
        }

        // GET: SMSApps/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSApp = await _context.SMSApp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sMSApp == null)
            {
                return NotFound();
            }

            return View(sMSApp);
        }

        // GET: SMSApps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SMSApps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,appid,appkey,SMSAppName,Describe")] SMSApp sMSApp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sMSApp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sMSApp);
        }

        // GET: SMSApps/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSApp = await _context.SMSApp.FindAsync(id);
            if (sMSApp == null)
            {
                return NotFound();
            }
            return View(sMSApp);
        }

        // POST: SMSApps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,appid,appkey,SMSAppName,Describe")] SMSApp sMSApp)
        {
            if (id != sMSApp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sMSApp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SMSAppExists(sMSApp.Id))
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
            return View(sMSApp);
        }

        // GET: SMSApps/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSApp = await _context.SMSApp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sMSApp == null)
            {
                return NotFound();
            }

            return View(sMSApp);
        }

        // POST: SMSApps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sMSApp = await _context.SMSApp.FindAsync(id);
            _context.SMSApp.Remove(sMSApp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SMSAppExists(string id)
        {
            return _context.SMSApp.Any(e => e.Id == id);
        }
    }
}
