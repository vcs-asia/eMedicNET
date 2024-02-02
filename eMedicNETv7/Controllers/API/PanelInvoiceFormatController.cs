using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eMedicEntityModel.Models.v1;
using Microsoft.AspNetCore.Authorization;
using eMedicNETv7.Data;

namespace eMedicNETv7.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PanelInvoiceFormatController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PanelInvoiceFormatController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await _context.GetPanels.ToListAsync();
            if (model.Count > 0)
            {
                return Ok(new ResponseModel { Message = "", Data = model, Flag = true });
            }
            return Ok(new ResponseModel { Message = "No record(s) found.", Data = null, Flag = false });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _context.GetPanels.FirstOrDefaultAsync(k => k.PnlAutid == id);
            if (model != null)
            {
                return Ok(new ResponseModel { Message = "", Data = model, Flag = true });
            }
            return Ok(new ResponseModel { Message = "No record(s) found.", Data = null, Flag = false });
        }

        [HttpPost]
        public async Task<IActionResult> Post([Bind("PifAutid,PifDescr,PifFpath,PifUsrid,PifState,PifCdate,PifUdate")] PanelInvoiceFormat model)
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
        public async Task<IActionResult> Put(int id, [Bind("PifAutid,PifDescr,PifFpath,PifUsrid,PifState,PifCdate,PifUdate")] PanelInvoiceFormat model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.PifAutid)
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
            var model = await _context.GetPanelInvoiceFormats.FindAsync(id);
            if (model != null)
            {
                _context.GetPanelInvoiceFormats.Remove(model);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool IsExists(int id)
        {
            return _context.GetPanelInvoiceFormats.Any(e => e.PifAutid == id);
        }
    }
}
