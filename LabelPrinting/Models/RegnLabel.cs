using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrinting.Models
{
    public class RegnLabel
    {
        public string PatName { get; set; }
        public string PatIcno { get; set; }
        public int PatAged { get; set; }
        public string PatGndr { get; set; }
        public DateTime PatBdat { get; set; }
        public int PatCops { get; set; }
    }
}
