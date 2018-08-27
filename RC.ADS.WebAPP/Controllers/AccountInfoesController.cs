using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RC.ADS.Data;
using RC.ADS.Data.Entity.AD_Account;
using RC.ADS.Data.Entity.AD_Menber;
using RC.ADS.Data.Enum;
using RC.ADS.WebAPP.Comm;

namespace RC.ADS.WebAPP.Controllers
{
    public class AccountInfoesController : Controller
    {
        private readonly DataContext _context;

        public AccountInfoesController(DataContext context)
        {
            _context = context;
        }

        // GET: AccountInfoes
        public async Task<IActionResult> Index(string owerId)
        {
            if (owerId == null)
            {
                //owerId = this.TempData["owerId"].ToString();
            }
            ViewBag.OwerName = _context.Menbers.FirstOrDefault(x => x.Id == owerId).Username;
            ViewBag.OwerId = owerId;
            return View(await _context.AccountInfos.Where(x => x.OwnerId == owerId).ToListAsync());
         
        }

        // GET: AccountInfoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountInfo = await _context.AccountInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountInfo == null)
            {
                return NotFound();
            }

            return View(accountInfo);
        }
    

        
        // GET: AccountInfoes/Create
        public IActionResult Create(string owerId)
        {
            ViewBag.OwerName = _context.Menbers.FirstOrDefault(x => x.Id == owerId).Username;
            AccountInfo accountInfo = new AccountInfo() { OwnerId = owerId };
           // var selectListEnum = _context.AccountInfoChangeTpyes.Select(x => new { Value = x.Id, Text = x.Name });
            //SelectList list = new SelectList(selectListEnum, "Value", "Text");
            //ViewBag.SelectListEnum = list;
            return View(accountInfo);
        }

        // POST: AccountInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( AccountInfo accountInfo)
        {
            if (ModelState.IsValid)
            {
                var menber = _context.Menbers.FirstOrDefault(x => x.Id == accountInfo.OwnerId);
                menber.AccountSum += accountInfo.Money;
                _context.Add(accountInfo);
                _context.Menbers.Update(menber);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Menbers");
            }
            return View(accountInfo);
        }

        // GET: AccountInfoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountInfo = await _context.AccountInfos.FindAsync(id);
            if (accountInfo == null)
            {
                return NotFound();
            }

            //var selectListEnum = EnumHelper.GetSelectListByEnum<AccountInfoChangeTypeEnum>();
            //SelectList list = new SelectList(selectListEnum, "Value", "Text", accountInfo.AccountInfoChangeTpyeId);
            //ViewBag.SelectListEnum = list;
            return View(accountInfo);
        }

        // POST: AccountInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Money,AccountInfoChangeTpye,Describe")] AccountInfo accountInfo)
        {
            if (id != accountInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountInfoExists(accountInfo.Id))
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
            return View(accountInfo);
        }

        // GET: AccountInfoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountInfo = await _context.AccountInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountInfo == null)
            {
                return NotFound();
            }

            return View(accountInfo);
        }

        // POST: AccountInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var accountInfo = await _context.AccountInfos.FindAsync(id);
            _context.AccountInfos.Remove(accountInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountInfoExists(string id)
        {
            return _context.AccountInfos.Any(e => e.Id == id);
        }
    }
}
