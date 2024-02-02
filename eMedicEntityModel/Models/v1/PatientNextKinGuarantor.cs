using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMedicEntityModel.Models.v1
{
    public class PatientNextKinGuarantor
    {
        public Patient Patient { get; set; } = null!;
        public PatientNextKin PatientNextKin { get; set; } = null!;
        public PatientGuarantor PatientGuarantor { get; set; } = null!;
    }
}
