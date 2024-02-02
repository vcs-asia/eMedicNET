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
    public class PatientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await _context.GetPatients.Include(p => p.Gender).Include(p => p.MaritalStatus).Include(p => p.PatientType).Include(p => p.Race).Include(p => p.Nationality).Include(p => p.BloodGroup).Include(p => p.Panel).Include(p => p.Relationship).ToListAsync();
            if (model.Count > 0)
            {
                return Ok(new ResponseModel { Message = "", Data = model, Flag = true });
            }
            return Ok(new ResponseModel { Message = "No record(s) found.", Data = null, Flag = false });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _context.GetPatients.FirstOrDefaultAsync(k => k.PrnAutid == id);
            if (model != null)
            {
                return Ok(new ResponseModel { Message = "", Data = model, Flag = true });
            }
            return Ok(new ResponseModel { Message = "No record(s) found.", Data = null, Flag = false });
        }

        [HttpPost]

        public async Task<IActionResult> Post([Bind("PrnAutid,PrnPname,PrnIcpno,PrnFoldr,PrnRegno,PrnRegdt,PrnDtdob,PrnGendr,PrnMrgst,PrnPtype,PrnFhnme,PrnAddr1,PrnAddr2,PrnAddr3,PrnEmail,PrnTeln1,PrnTeln2,PrnTelhp,PrnOccup,PrnIrace,PrnNtion,PrnBgrop,PrnPhist,PrnPanel,PrnPstas,PrnEmpno,PrnRelat,PrnRelto,PrnPnlol,PrnCstcn,PrnDeprt,PrnPodtl,PrnRmrks,PrnAlrgy,PrnPhoto,PrnRefby,PrnRefcl,PrnTrpln,PrnUsrid,PrnCdate,PrnUdate")] Patient model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(model);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        return Ok(new ResponseModel { Message = "Record has been saved successfully.", Data = model, Flag = true });
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status415UnsupportedMediaType, new ResponseModel { Message = "Failed to save", Data = null, Flag = false });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status417ExpectationFailed, new ResponseModel { Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, Data = null, Flag = false });
                }

            }
            else
            {
                return Ok(new ResponseModel { Message = string.Join(",", ModelState.Values.SelectMany(k => k.Errors).Select(k => k.ErrorMessage)), Flag = false, Data = null });
            }
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Put(int id, [Bind("PrnAutid,PrnPname,PrnIcpno,PrnFoldr,PrnRegno,PrnRegdt,PrnDtdob,PrnGendr,PrnMrgst,PrnPtype,PrnFhnme,PrnAddr1,PrnAddr2,PrnAddr3,PrnEmail,PrnTeln1,PrnTeln2,PrnTelhp,PrnOccup,PrnIrace,PrnNtion,PrnBgrop,PrnPhist,PrnPanel,PrnPstas,PrnEmpno,PrnRelat,PrnRelto,PrnPnlol,PrnCstcn,PrnDeprt,PrnPodtl,PrnRmrks,PrnAlrgy,PrnPhoto,PrnRefby,PrnRefcl,PrnTrpln,PrnUsrid,PrnCdate,PrnUdate")] Patient model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.PrnAutid)
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
