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
    public class OrderStatusChangesController : Controller
    {
        private readonly DataContext _context;

        public OrderStatusChangesController(DataContext context)
        {
            _context = context;
        }

        // GET: OrderStatusChanges
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.OrderStatusChanges.Include(o => o.OrderEntity);
            return View(await dataContext.ToListAsync());
        }

        // GET: OrderStatusChanges/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatusChange = await _context.OrderStatusChanges
                .Include(o => o.OrderEntity)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderStatusChange == null)
            {
                return NotFound();
            }

            return View(orderStatusChange);
        }

        // GET: OrderStatusChanges/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            return View();
        }

        // POST: OrderStatusChanges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderId,Status,Creatime")] OrderStatusChange orderStatusChange)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderStatusChange);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderStatusChange.OrderId);
            return View(orderStatusChange);
        }

        // GET: OrderStatusChanges/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatusChange = await _context.OrderStatusChanges.FindAsync(id);
            if (orderStatusChange == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderStatusChange.OrderId);
            return View(orderStatusChange);
        }

        // POST: OrderStatusChanges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,OrderId,Status,Creatime")] OrderStatusChange orderStatusChange)
        {
            if (id != orderStatusChange.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderStatusChange);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderStatusChangeExists(orderStatusChange.Id))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderStatusChange.OrderId);
            return View(orderStatusChange);
        }

        // GET: OrderStatusChanges/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatusChange = await _context.OrderStatusChanges
                .Include(o => o.OrderEntity)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderStatusChange == null)
            {
                return NotFound();
            }

            return View(orderStatusChange);
        }

        // POST: OrderStatusChanges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var orderStatusChange = await _context.OrderStatusChanges.FindAsync(id);
            _context.OrderStatusChanges.Remove(orderStatusChange);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderStatusChangeExists(string id)
        {
            return _context.OrderStatusChanges.Any(e => e.Id == id);
        }
    }
}
