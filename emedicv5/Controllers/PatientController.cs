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
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Patient
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GetPatients.Include(p => p.BloodGroup).Include(p => p.Gender).Include(p => p.MaritalStatus).Include(p => p.Nationality).Include(p => p.Panel).Include(p => p.PanelStatus).Include(p => p.PatientType).Include(p => p.Race).Include(p => p.Relationship);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Patient/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.GetPatients
                .Include(p => p.BloodGroup)
                .Include(p => p.Gender)
                .Include(p => p.MaritalStatus)
                .Include(p => p.Nationality)
                .Include(p => p.Panel)
                .Include(p => p.PanelStatus)
                .Include(p => p.PatientType)
                .Include(p => p.Race)
                .Include(p => p.Relationship)
                .FirstOrDefaultAsync(m => m.PrnAutid == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patient/Create
        public IActionResult Create()
        {
            ViewData["PrnBgrop"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid");
            ViewData["PrnGendr"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid");
            ViewData["PrnMrgst"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid");
            ViewData["PrnNtion"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid");
            ViewData["PrnPanel"] = new SelectList(_context.GetPanels, "PnlAutid", "PnlAutid");
            ViewData["PrnPstas"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid");
            ViewData["PrnPtype"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid");
            ViewData["PrnIrace"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid");
            ViewData["PrnRelat"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid");
            return View();
        }

        // POST: Patient/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrnAutid,PrnPname,PrnIcpno,PrnFoldr,PrnRegno,PrnRegdt,PrnDtdob,PrnGendr,PrnMrgst,PrnPtype,PrnFhnme,PrnAddr1,PrnAddr2,PrnAddr3,PrnEmail,PrnTeln1,PrnTeln2,PrnTelhp,PrnOccup,PrnIrace,PrnNtion,PrnBgrop,PrnPhist,PrnPanel,PrnPstas,PrnEmpno,PrnRelat,PrnRelto,PrnPnlol,PrnCstcn,PrnDeprt,PrnPodtl,PrnRmrks,PrnAlrgy,PrnUsrid,PrnPhoto,PrnCdate,PrnUdate")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrnBgrop"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnBgrop);
            ViewData["PrnGendr"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnGendr);
            ViewData["PrnMrgst"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnMrgst);
            ViewData["PrnNtion"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnNtion);
            ViewData["PrnPanel"] = new SelectList(_context.GetPanels, "PnlAutid", "PnlAutid", patient.PrnPanel);
            ViewData["PrnPstas"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnPstas);
            ViewData["PrnPtype"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnPtype);
            ViewData["PrnIrace"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnIrace);
            ViewData["PrnRelat"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnRelat);
            return View(patient);
        }

        // GET: Patient/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.GetPatients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            ViewData["PrnBgrop"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnBgrop);
            ViewData["PrnGendr"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnGendr);
            ViewData["PrnMrgst"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnMrgst);
            ViewData["PrnNtion"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnNtion);
            ViewData["PrnPanel"] = new SelectList(_context.GetPanels, "PnlAutid", "PnlAutid", patient.PrnPanel);
            ViewData["PrnPstas"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnPstas);
            ViewData["PrnPtype"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnPtype);
            ViewData["PrnIrace"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnIrace);
            ViewData["PrnRelat"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnRelat);
            return View(patient);
        }

        // POST: Patient/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PrnAutid,PrnPname,PrnIcpno,PrnFoldr,PrnRegno,PrnRegdt,PrnDtdob,PrnGendr,PrnMrgst,PrnPtype,PrnFhnme,PrnAddr1,PrnAddr2,PrnAddr3,PrnEmail,PrnTeln1,PrnTeln2,PrnTelhp,PrnOccup,PrnIrace,PrnNtion,PrnBgrop,PrnPhist,PrnPanel,PrnPstas,PrnEmpno,PrnRelat,PrnRelto,PrnPnlol,PrnCstcn,PrnDeprt,PrnPodtl,PrnRmrks,PrnAlrgy,PrnUsrid,PrnPhoto,PrnCdate,PrnUdate")] Patient patient)
        {
            if (id != patient.PrnAutid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.PrnAutid))
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
            ViewData["PrnBgrop"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnBgrop);
            ViewData["PrnGendr"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnGendr);
            ViewData["PrnMrgst"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnMrgst);
            ViewData["PrnNtion"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnNtion);
            ViewData["PrnPanel"] = new SelectList(_context.GetPanels, "PnlAutid", "PnlAutid", patient.PrnPanel);
            ViewData["PrnPstas"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnPstas);
            ViewData["PrnPtype"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnPtype);
            ViewData["PrnIrace"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnIrace);
            ViewData["PrnRelat"] = new SelectList(_context.GetParameters, "PrmAutid", "PrmAutid", patient.PrnRelat);
            return View(patient);
        }

        // GET: Patient/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.GetPatients
                .Include(p => p.BloodGroup)
                .Include(p => p.Gender)
                .Include(p => p.MaritalStatus)
                .Include(p => p.Nationality)
                .Include(p => p.Panel)
                .Include(p => p.PanelStatus)
                .Include(p => p.PatientType)
                .Include(p => p.Race)
                .Include(p => p.Relationship)
                .FirstOrDefaultAsync(m => m.PrnAutid == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var patient = await _context.GetPatients.FindAsync(id);
            _context.GetPatients.Remove(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(string id)
        {
            return _context.GetPatients.Any(e => e.PrnAutid == id);
        }
    }
}
