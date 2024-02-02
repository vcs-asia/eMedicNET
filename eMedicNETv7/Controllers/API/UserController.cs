using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using eMedicEntityModel.Models.v1;
using eMedicNETv7.Data;

namespace eMedicNETv7.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await _context.Users.ToListAsync();
            if (model.Count > 0)
            {
                return Ok(new ResponseModel { Message = "", Data = model, Flag = true });
            }
            return Ok(new ResponseModel { Message = "No record(s) found.", Data = null, Flag = false });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var model = await _context.Users.FirstOrDefaultAsync(k => k.Id == id);
            if (model != null)
            {
                return Ok(new ResponseModel { Message = "", Data = model, Flag = true });
            }
            return Ok(new ResponseModel { Message = "No record(s) found.", Data = null, Flag = false });
        }

        [HttpPost]

        public async Task<IActionResult> Post([Bind("UsrAutid,UsrEmail,UsrUname,UsrPaswd")] UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(new ApplicationUser { Email = model.UsrEmail, UserName = model.UsrUname, EmailConfirmed = true }, model.UsrPaswd);
                if (result.Succeeded)
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
