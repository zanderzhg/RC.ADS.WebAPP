using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RC.ADS.Data;
using RC.ADS.Data.Entity.AD_Account;

namespace RC.ADS.WebAPP.Controllers
{
    public class AccountInfoChangeTypesController : Controller
    {
        private readonly DataContext _context;

        public AccountInfoChangeTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: AccountInfoChangeTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AccountInfoChangeTpyes.ToListAsync());
        }

        // GET: AccountInfoChangeTypes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountInfoChangeType = await _context.AccountInfoChangeTpyes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountInfoChangeType == null)
            {
                return NotFound();
            }

            return View(accountInfoChangeType);
        }

        // GET: AccountInfoChangeTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccountInfoChangeTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Describe")] AccountInfoChangeType accountInfoChangeType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountInfoChangeType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accountInfoChangeType);
        }

        // GET: AccountInfoChangeTypes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountInfoChangeType = await _context.AccountInfoChangeTpyes.FindAsync(id);
            if (accountInfoChangeType == null)
            {
                return NotFound();
            }
            return View(accountInfoChangeType);
        }

        // POST: AccountInfoChangeTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Describe")] AccountInfoChangeType accountInfoChangeType)
        {
            if (id != accountInfoChangeType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountInfoChangeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountInfoChangeTypeExists(accountInfoChangeType.Id))
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
            return View(accountInfoChangeType);
        }

        // GET: AccountInfoChangeTypes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountInfoChangeType = await _context.AccountInfoChangeTpyes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountInfoChangeType == null)
            {
                return NotFound();
            }

            return View(accountInfoChangeType);
        }

        // POST: AccountInfoChangeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var accountInfoChangeType = await _context.AccountInfoChangeTpyes.FindAsync(id);
            _context.AccountInfoChangeTpyes.Remove(accountInfoChangeType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountInfoChangeTypeExists(string id)
        {
            return _context.AccountInfoChangeTpyes.Any(e => e.Id == id);
        }
    }
}
