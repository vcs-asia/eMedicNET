using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrinting.Models
{
    public class MedicineLabel
    {
        public string PatName { get; set; }
        public string MedName { get; set; }
        public string VsdDesc { get; set; }
        public int VsdQnty { get; set; }
        public int VstAtid { get; set; }
        public string MedCatn { get; set; }
    }
}
