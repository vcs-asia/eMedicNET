using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
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
    public class ParameterController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ParameterController> _logger;
        public ParameterController(IConfiguration configuration, ApplicationDbContext context, ILogger<ParameterController> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _context.GetParameters.ToListAsync();
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("PrmAutid,PrmPdesc,PrmPtype,PrmState,PrmUsrid,PrmCdate,PrmUdate")] Parameter model)
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

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.GetParameters.FirstOrDefaultAsync(k => k.PrmAutid == id);
            if (model != null)
            {
                return View(model);
            }
            return NoContent();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrmAutid,PrmPdesc,PrmPtype,PrmState,PrmUsrid,PrmCdate,PrmUdate")] Parameter model)
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
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _context.GetParameters.FirstOrDefaultAsync(k => k.PrmAutid == id);
            if (model != null)
            {
                return View(model);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await _context.GetParameters.FirstOrDefaultAsync(k => k.PrmAutid == id);
            if (model != null)
            {
                _context.GetParameters.Remove(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}