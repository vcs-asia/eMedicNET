using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eMedicv5.Data;
using eMedicNETEMv1.Models;

namespace eMedicv5.Controllers
{
    public class LetterTemplateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LetterTemplateController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LetterTemplate
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetLetterTemplates.ToListAsync());
        }

        // GET: LetterTemplate/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var letterTemplate = await _context.GetLetterTemplates
                .FirstOrDefaultAsync(m => m.LtmAutid == id);
            if (letterTemplate == null)
            {
                return NotFound();
            }

            return View(letterTemplate);
        }

        // GET: LetterTemplate/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LetterTemplate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LtmAutid,LtmCntnt,LtmUsrid,LtmCdate,LtmUdate")] LetterTemplate letterTemplate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(letterTemplate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(letterTemplate);
        }

        // GET: LetterTemplate/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var letterTemplate = await _context.GetLetterTemplates.FindAsync(id);
            if (letterTemplate == null)
            {
                return NotFound();
            }
            return View(letterTemplate);
        }

        // POST: LetterTemplate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LtmAutid,LtmCntnt,LtmUsrid,LtmCdate,LtmUdate")] LetterTemplate letterTemplate)
        {
            if (id != letterTemplate.LtmAutid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(letterTemplate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LetterTemplateExists(letterTemplate.LtmAutid))
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
            return View(letterTemplate);
        }

        // GET: LetterTemplate/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var letterTemplate = await _context.GetLetterTemplates
                .FirstOrDefaultAsync(m => m.LtmAutid == id);
            if (letterTemplate == null)
            {
                return NotFound();
            }

            return View(letterTemplate);
        }

        // POST: LetterTemplate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var letterTemplate = await _context.GetLetterTemplates.FindAsync(id);
            _context.GetLetterTemplates.Remove(letterTemplate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LetterTemplateExists(string id)
        {
            return _context.GetLetterTemplates.Any(e => e.LtmAutid == id);
        }
    }
}
