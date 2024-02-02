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
    public class ReportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Patient")]
        public async Task<IActionResult> Patient()
        {
            return Ok(await _context.GetPatients.ToListAsync());
        }

        [HttpGet("Audit")]
        public async Task<IActionResult> Audit()
        {
            return Ok(await _context.GetPatients.ToListAsync());
        }

        [HttpGet("PanelSummary")]
        public async Task<IActionResult> PanelSummary()
        {
            return Ok(await _context.GetPatients.ToListAsync());
        }

        [HttpGet("PanelVisitSummary")]
        public async Task<IActionResult> PanelVisitSummary()
        {
            return Ok(await _context.GetPatients.ToListAsync());
        }

        [HttpGet("DailyTrafficReport")]
        public async Task<IActionResult> DailyTrafficReport()
        {
            return Ok(await _context.GetPatients.ToListAsync());
        }

        [HttpGet("DispensingReport")]
        public async Task<IActionResult> DispensingReport()
        {
            return Ok(await _context.GetPatients.ToListAsync());
        }

        [HttpGet("PanelPaymentDetail")]
        public async Task<IActionResult> PanelPaymentDetail()
        {
            return Ok(await _context.GetPatients.ToListAsync());
        }

        [HttpGet("PaymentDetail")]
        public async Task<IActionResult> PaymentDetail()
        {
            return Ok(await _context.GetPatients.ToListAsync());
        }

        [HttpGet("PsyDrugDetail")]
        public async Task<IActionResult> PsyDrugDetail()
        {
            return Ok(await _context.GetPatients.ToListAsync());
        }

        [HttpGet("Grn")]
        public async Task<IActionResult> Grn()
        {
            return Ok(await _context.GetPatients.ToListAsync());
        }

        [HttpGet("CurrentStock")]
        public async Task<IActionResult> CurrentStock()
        {
            return Ok(await _context.GetPatients.ToListAsync());
        }

        [HttpGet("CurrentStockValue")]
        public async Task<IActionResult> CurrentStockValue()
        {
            return Ok(await _context.GetPatients.ToListAsync());
        }

        [HttpGet("CurrentStockBatch")]
        public async Task<IActionResult> CurrentStockBatch()
        {
            return Ok(await _context.GetPatients.ToListAsync());
        }

        [HttpGet("StockListSP")]
        public async Task<IActionResult> StockListSP()
        {
            return Ok(await _context.GetPatients.ToListAsync());
        }

        [HttpGet("IssueValue")]
        public async Task<IActionResult> IssueValue()
        {
            return Ok(await _context.GetPatients.ToListAsync());
        }

        [HttpGet("Reorder")]
        public async Task<IActionResult> Reorder()
        {
            return Ok(await _context.GetPatients.ToListAsync());
        }

        [HttpGet("Expiring")]
        public async Task<IActionResult> Expiring()
        {
            return Ok(await _context.GetPatients.ToListAsync());
        }

        [HttpGet("Markup")]
        public async Task<IActionResult> Markup()
        {
            return Ok(await _context.GetPatients.ToListAsync());
        }

        [HttpGet("OutletCS")]
        public async Task<IActionResult> OutletCS()
        {
            return Ok(await _context.GetPatients.ToListAsync());
        }

        [HttpGet("OutstandingPO")]
        public async Task<IActionResult> OutstandingPO()
        {
            return Ok(await _context.GetPatients.ToListAsync());
        }
    }
}
