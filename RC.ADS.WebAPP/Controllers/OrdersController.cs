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
    public class OrdersController : Controller
    {
        private readonly DataContext _context;

        public OrdersController(DataContext context)
        {
            _context = context;
        }
        // GET: Orders
        public IActionResult Index(string owerId)
        {
            if (owerId == null)
            {
                //owerId = this.TempData["owerId"].ToString();
            }
            ViewBag.OwerName = _context.Menbers.FirstOrDefault(x => x.Id == owerId).Username;
            ViewBag.OwerId = owerId;
            return View(_context.Orders.Where(x => x.OwnerId == owerId).ToList());
        }
        public IActionResult ShowOrderStatus(string orderId)
        {
            ViewBag.OrderName = _context.Orders.FirstOrDefault(x=>x.Id==orderId).OrderName;
            return View(_context.OrderStatusChanges.Where(x => x.OrderId == orderId).ToList());
        }
      

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create(string owerId)
        {
            Order vm = new Order() { OwnerId = owerId };
            //var selectListEnum = _context.OrderStatus.Select(x => new { Value = x.Id, Text = x.ChineseName });
            //SelectList list = new SelectList(selectListEnum, "Value", "Text");
            //ViewBag.SelectListEnum = list;

            return View(vm);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                order.CreateTime = DateTime.Now;
                order.LastUpdateTime = DateTime.Now;
                _context.Add(order);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index), new { owerId = order.OwnerId });
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
          

            if (order == null)
            {
                return NotFound();
            }
            //var selectListEnum = _context.OrderStatus.Select(x => new { Value = x.Id, Text = x.ChineseName });
            //SelectList list = new SelectList(selectListEnum, "Value", "Text", order.OrderStatusId);
            //ViewBag.SelectListEnum = list;
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    order.LastUpdateTime = DateTime.Now;
                    OrderStatusChange orderStatusChange = new OrderStatusChange()
                    {
                        OrderId = order.Id,
                        OrderStatu = order.OrderStatu,
                        Price = order.Price,
                        Description = order.Description,
                        CreateTime = order.LastUpdateTime,
                    };
                    _context.OrderStatusChanges.Add(orderStatusChange);
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { owerId = order.OwnerId });
            }
            return View(order);
        }

 
        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(string id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
