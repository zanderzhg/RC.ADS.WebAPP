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
    public class SMSAppTemplatesController : Controller
    {
        private readonly DataContext _context;

        public SMSAppTemplatesController(DataContext context)
        {
            _context = context;
        }

        // GET: SMSAppTemplates
        public async Task<IActionResult> Index()
        {
            return View(await _context.SMSAppTemplates.ToListAsync());
        }

        // GET: SMSAppTemplates/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSAppTemplate = await _context.SMSAppTemplates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sMSAppTemplate == null)
            {
                return NotFound();
            }

            return View(sMSAppTemplate);
        }

        // GET: SMSAppTemplates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SMSAppTemplates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Appid,AppName,Appkey,TemplateId,TemplateType,SMSAppTemplateName,Describe,CreateTime")] SMSAppTemplate sMSAppTemplate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sMSAppTemplate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sMSAppTemplate);
        }

        // GET: SMSAppTemplates/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSAppTemplate = await _context.SMSAppTemplates.FindAsync(id);
            if (sMSAppTemplate == null)
            {
                return NotFound();
            }
            return View(sMSAppTemplate);
        }

        // POST: SMSAppTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Appid,AppName,Appkey,TemplateId,TemplateType,SMSAppTemplateName,Describe,CreateTime")] SMSAppTemplate sMSAppTemplate)
        {
            if (id != sMSAppTemplate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sMSAppTemplate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SMSAppTemplateExists(sMSAppTemplate.Id))
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
            return View(sMSAppTemplate);
        }

        // GET: SMSAppTemplates/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSAppTemplate = await _context.SMSAppTemplates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sMSAppTemplate == null)
            {
                return NotFound();
            }

            return View(sMSAppTemplate);
        }

        // POST: SMSAppTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sMSAppTemplate = await _context.SMSAppTemplates.FindAsync(id);
            _context.SMSAppTemplates.Remove(sMSAppTemplate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SMSAppTemplateExists(string id)
        {
            return _context.SMSAppTemplates.Any(e => e.Id == id);
        }
    }
}
