using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using eMedicEntityModel.Models.v1;
using eMedicNETv7.Data;

namespace eMedicNETv7.ViewComponents
{
    [ViewComponent(Name = "VendorInvoiceComponent")]
    public class VendorInvoiceViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public VendorInvoiceViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            //var model = _context.GetVendorInvoices;
            return View();
        }
    }
}
