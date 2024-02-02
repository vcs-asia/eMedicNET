using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eMedicEntityModel.Models.v1;
using Microsoft.AspNetCore.Authorization;
using eMedicNETv6.Data;

namespace eMedicNETv6.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ParameterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ParameterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await _context.GetParameters.ToListAsync();
            if (model.Count > 0)
            {
                return Ok(new ResponseModel { Message = "", Data = model, Flag = true });
            }
            return Ok(new ResponseModel { Message = "No record(s) found.", Data = null, Flag = false });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _context.GetParameters.FirstOrDefaultAsync(k => k.PrmAutid == id);
            if (model != null)
            {
                return Ok(new ResponseModel { Message = "", Data = model, Flag = true });
            }
            return Ok(new ResponseModel { Message = "No record(s) found.", Data = null, Flag = false });
        }

        [HttpGet("GetByType/{id}")]
        public async Task<IActionResult> GetByType(string id)
        {
            var model = await _context.GetParameters.Where(k => k.PrmPtype.Equals(id) && k.PrmState == true).ToListAsync();
            if (model.Count > 0)
            {
                return Ok(new ResponseModel { Message = "", Data = model, Flag = true });
            }
            return Ok(new ResponseModel { Message = "No record(s) found.", Data = null, Flag = false });
        }

        [HttpPost]
        public async Task<IActionResult> Post([Bind("PrmAutid,PrmPdesc,PrmPtype,PrmState,PrmUsrid,PrmCdate,PrmUdate")] Parameter model)
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
        public async Task<IActionResult> Put(int id, [Bind("PrmAutid,PrmPdesc,PrmPtype,PrmState,PrmUsrid,PrmCdate,PrmUdate")] Parameter model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.PrmAutid)
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
            var model = await _context.GetParameters.FirstOrDefaultAsync(k => k.PrmAutid.Equals(id));
            if (model != null)
            {
                _context.Remove(model);
                return Ok(await _context.SaveChangesAsync());
            }
            else
            {
                return NotFound();
            }
        }
    }
}
