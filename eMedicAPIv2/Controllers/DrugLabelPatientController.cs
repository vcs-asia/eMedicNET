using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using eMedicAPIv2.Data;
using eMedicEntityModel.Models.v1;
using Microsoft.AspNetCore.Authorization;

namespace eMedicAPIv2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class DrugLabelController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DrugLabelController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await _context.GetProducts.ToListAsync();
            if (model.Count > 0)
            {
                return Ok(new ResponseModel { Message = "", Data = model, Flag = true });
            }
            return Ok(new ResponseModel { Message = "No record(s) found.", Data = null, Flag = false });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _context.GetProducts.FirstOrDefaultAsync(k => k.ProAutid == id);
            if (model != null)
            {
                return Ok(new ResponseModel { Message = "", Data = model, Flag = true });
            }
            return Ok(new ResponseModel { Message = "No record(s) found.", Data = null, Flag = false });
        }

        [HttpPost]

        public async Task<IActionResult> Post([Bind("PrnAutid,PrnPname,PrnIcpno,PrnFoldr,PrnRegno,PrnRegdt,PrnDtdob,PrnGendr,PrnMrgst,PrnPtype,PrnFhnme,PrnAddr1,PrnAddr2,PrnAddr3,PrnEmail,PrnTeln1,PrnTeln2,PrnTelhp,PrnOccup,PrnIrace,PrnNtion,PrnBgrop,PrnPhist,PrnPanel,PrnPstas,PrnEmpno,PrnRelat,PrnRelto,PrnPnlol,PrnCstcn,PrnDeprt,PrnPodtl,PrnRmrks,PrnAlrgy,PrnUsrid,PrnPhoto,PrnCdate,PrnUdate")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(patient);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Put(int id, [Bind("PrnAutid,PrnPname,PrnIcpno,PrnFoldr,PrnRegno,PrnRegdt,PrnDtdob,PrnGendr,PrnMrgst,PrnPtype,PrnFhnme,PrnAddr1,PrnAddr2,PrnAddr3,PrnEmail,PrnTeln1,PrnTeln2,PrnTelhp,PrnOccup,PrnIrace,PrnNtion,PrnBgrop,PrnPhist,PrnPanel,PrnPstas,PrnEmpno,PrnRelat,PrnRelto,PrnPnlol,PrnCstcn,PrnDeprt,PrnPodtl,PrnRmrks,PrnAlrgy,PrnUsrid,PrnPhoto,PrnCdate,PrnUdate")] Patient patient)
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
                    if (!IsExists(patient.PrnAutid))
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
            return Ok(patient);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(string id)
        {
            var patient = await _context.GetPatients.FindAsync(id);
            if (patient != null)
            {
                _context.GetPatients.Remove(patient);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool IsExists(int id)
        {
            return _context.GetPatients.Any(e => e.PrnAutid == id);
        }
    }
}
