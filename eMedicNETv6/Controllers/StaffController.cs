﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

using eMedicEntityModel.Models.v1;
using eMedicNETv6.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using eMedicNETv6.Data;

namespace eMedicNETv6.Controllers
{
	[ApiExplorerSettings(IgnoreApi = true)]
	[Authorize]
	public class StaffController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly ApplicationDbContext _context;
		private readonly ILogger<StaffController> _logger;
		public StaffController(IConfiguration configuration, ApplicationDbContext context, ILogger<StaffController> logger)
		{
			_configuration = configuration;
			_logger = logger;
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var model = await _context.GetStaffs.ToListAsync();
			return View(model);
		}

		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Add([Bind("StfAutid,StfSname,StfIcppt,StfSpeid,StfStsex,StfTelhp,StfEmail,StfQuali,StfRmrks,StfCatid,StfPhoto,StfSpath,StfUsrid,StfState,StfCdate,StfUdate")] Staff model)
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
			var model = await _context.GetStaffs.FirstOrDefaultAsync(k => k.StfAutid == id);
			if (model != null)
			{
				return View(model);
			}
			return NoContent();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("StfAutid,StfSname,StfIcppt,StfSpeid,StfStsex,StfTelhp,StfEmail,StfQuali,StfRmrks,StfCatid,StfPhoto,StfSpath,StfUsrid,StfState,StfCdate,StfUdate")] Staff model)
		{
			if (ModelState.IsValid)
			{
				if (id != model.StfAutid)
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
			var model = await _context.GetStaffs.FirstOrDefaultAsync(k => k.StfAutid == id);
			if (model != null)
			{
				return View(model);
			}
			return NotFound();
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var model = await _context.GetStaffs.FirstOrDefaultAsync(k => k.StfAutid == id);
			if (model != null)
			{
				_context.GetStaffs.Remove(model);
				await _context.SaveChangesAsync();

				return RedirectToAction("Index");
			}

			return NotFound();
		}
	}
}
