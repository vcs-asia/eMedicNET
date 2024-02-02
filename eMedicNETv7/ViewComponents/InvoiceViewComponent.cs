using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using eMedicEntityModel.Models.v1;
using eMedicNETv7.Data;

namespace eMedicEntityModel.ViewComponents
{
    [ViewComponent(Name = "InvoiceComponent")]
    public class InvoiceViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public InvoiceViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        /*
        public IViewComponentResult Invoke(object parameter)
        {
            var model = _context.GetInvoices;

            var companyProfile = _context.GetCompanyProfiles.FirstOrDefault();

            if (companyProfile != null)
            {
                //ViewData["Owner"] = companyProfile.Comsname;
            }

            return View(model);
        }*/
    }
}
