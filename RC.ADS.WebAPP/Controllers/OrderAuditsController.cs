using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RC.ADS.Data;
using RC.ADS.Data.Entity.AD_Order;

namespace RC.ADS.WebAPP.Controllers
{
    public class OrderAuditsController : Controller
    {
        private readonly DataContext _context;

        public OrderAuditsController(DataContext context)
        {
            _context = context;
        }

        // GET: OrderAudits
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderAudits.ToListAsync());
        }

        // GET: OrderAudits/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderAudit = await _context.OrderAudits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderAudit == null)
            {
                return NotFound();
            }

            return View(orderAudit);
        }

        // GET: OrderAudits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderAudits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderStatus,Creatime")] OrderAudit orderAudit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderAudit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderAudit);
        }

        // GET: OrderAudits/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderAudit = await _context.OrderAudits.FindAsync(id);
            if (orderAudit == null)
            {
                return NotFound();
            }
            return View(orderAudit);
        }

        // POST: OrderAudits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,OrderStatus,Creatime")] OrderAudit orderAudit)
        {
            if (id != orderAudit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderAudit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderAuditExists(orderAudit.Id))
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
            return View(orderAudit);
        }

        // GET: OrderAudits/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderAudit = await _context.OrderAudits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderAudit == null)
            {
                return NotFound();
            }

            return View(orderAudit);
        }

        // POST: OrderAudits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var orderAudit = await _context.OrderAudits.FindAsync(id);
            _context.OrderAudits.Remove(orderAudit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderAuditExists(string id)
        {
            return _context.OrderAudits.Any(e => e.Id == id);
        }
    }
}
