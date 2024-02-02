using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMedicEntityModel.Models.v1
{
    public class Investigation
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IngAutid { get; set; }

        [Display(Name = "Description")]
        [StringLength(200)]
        public string IngDescr { get; set; } = string.Empty;

        [StringLength(255)]
        public string IngUsrid { get; set; } = string.Empty;

        public DateTime IngCdate { get; set; }

        public DateTime? IngUdate { get; set; }
    }
}
