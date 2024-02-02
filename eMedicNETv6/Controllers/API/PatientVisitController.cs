using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eMedicEntityModel.Models.v1;
using Microsoft.AspNetCore.Authorization;
using eMedicNETv6.Data;

namespace eMedicNETv6.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PatientVisitController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientVisitController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await _context.GetPatientVisits.ToListAsync();
            if (model.Count > 0)
            {
                return Ok(new ResponseModel { Message = "", Data = model, Flag = true });
            }
            return Ok(new ResponseModel { Message = "No record(s) found.", Data = null, Flag = false });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _context.GetPatientVisits.FirstOrDefaultAsync(k => k.PvtVstid == id);
            if (model != null)
            {
                return Ok(new ResponseModel { Message = "", Data = model, Flag = true });
            }
            return Ok(new ResponseModel { Message = "No record(s) found.", Data = null, Flag = false });
        }

        [HttpPost]

        public async Task<IActionResult> Post([Bind("PvtVstid,PvtPatid,PvtQdate,PvtQstat,PvtVdate,PvtVtime,PvtDocid,PvtRmrks,PvtTtamt,PvtPnlid,PvtDscid,PvtPdamt,PvtDcamt,PvtBilno,PvtState,PvtTimot,PvtAptdt,PvtMcday,PvtMcdat,PvtWeigh,PvtHeigh,PvtDcbmi,PvtUsrid,PvtLuser,PvtCdate,PvtUdate")] PatientVisit model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return Ok(new ResponseModel { Message = "Record has been saved successfully.", Data = model, Flag = true });
                }
                else
                {
                    return Ok(new ResponseModel { Message = "Failed to save", Data = null, Flag = false });
                }
            }
            else
            {
                return Ok(new ResponseModel { Message = string.Join(",", ModelState.Values.SelectMany(k => k.Errors).Select(k => k.ErrorMessage)), Flag = false, Data = null });
            }
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Put(int id, [Bind("PvtVstid,PvtPatid,PvtQdate,PvtQstat,PvtVdate,PvtVtime,PvtDocid,PvtRmrks,PvtTtamt,PvtPnlid,PvtDscid,PvtPdamt,PvtDcamt,PvtBilno,PvtState,PvtTimot,PvtAptdt,PvtMcday,PvtMcdat,PvtWeigh,PvtHeigh,PvtDcbmi,PvtUsrid,PvtLuser,PvtCdate,PvtUdate")] PatientVisit model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.PvtVstid)
                {
                    return NotFound();
                }

                try
                {
                    _context.Update(model);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        return Ok(new ResponseModel { Message = "Record has been saved successfully.", Data = model, Flag = true });
                    }
                    else
                    {
                        return Ok(new ResponseModel { Message = "Failed to save", Data = null, Flag = false });
                    }
                }
                catch (DbUpdateException ex)
                {
                    return Ok(new ResponseModel { Message = ex.Message.ToString(), Data = null, Flag = false });
                }
            }
            else
            {
                return Ok(new ResponseModel { Message = string.Join(",", ModelState.Values.SelectMany(k => k.Errors).Select(k => k.ErrorMessage)), Flag = false, Data = null });
            }
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(string id)
        {
            var model = await _context.GetPatients.FindAsync(id);
            if (model != null)
            {
                _context.GetPatients.Remove(model);
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
