using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class Letter
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int LtrAutid { get; set; }

        [Display(Name = "Patient")]
        
        public int LtrPatid { get; set; }

        [ForeignKey("LtrPatid")]
        public Patient? Patient { get; set; }

        [Display(Name = "Doctor")]
        
        public int LtrDocid { get; set; }

        [ForeignKey("LtrDocid")]
        public Staff? Doctor { get; set; }

        [StringLength(200)]
        public string LtrUsrid { get; set; } = string.Empty;

        public DateTime LtrCdate { get; set; }

        public DateTime? LtrUdate { get; set; }
    }
}
