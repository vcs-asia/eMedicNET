using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrinting.Models
{
    public class RegnLabelResponse
    {
        public bool Flag { get; set; }
        public string Message { get; set; }
        public List<RegnLabel> RegnLabels { get; set; }
    }
}
