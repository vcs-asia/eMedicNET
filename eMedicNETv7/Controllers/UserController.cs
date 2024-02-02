using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

using eMedicNETv7.Data;
using eMedicEntityModel.Models.v1;
using eMedicNETv7.Extensions;

namespace eMedicNETv7.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserController> _logger;
        public UserController(IConfiguration configuration, ApplicationDbContext context, ILogger<UserController> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _context.Users.ToListAsync();
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("UsrAutid,UsrEmail,UsrUname,UsrPaswd")] UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(model);
                    await _context.SaveChangesAsync();

                }
                catch (DbException ex)
                {
                    ModelState.AddModelError("", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", string.Join(";", ModelState.Values.SelectMany(k => k.Errors).Select(k => k.ErrorMessage)));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await _context.Users.FirstOrDefaultAsync(k => k.Id == id);
            if (model != null)
            {
                return View(model);
            }
            return NoContent();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UsrAutid,UsrEmail,UsrUname,UsrPaswd")] UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.UsrAutid)
                {
                    return NotFound();
                }

                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", string.Join(";", ModelState.Values.SelectMany(k => k.Errors).Select(k => k.ErrorMessage)));
            }
            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var model = await _context.Users.FirstOrDefaultAsync(k => k.Id == id);
            if (model != null)
            {
                return View(model);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var model = await _context.Users.FirstOrDefaultAsync(k => k.Id == id);
            if (model != null)
            {
                _context.Users.Remove(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}
