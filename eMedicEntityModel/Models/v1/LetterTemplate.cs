using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class LetterTemplate
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int LtmAutid { get; set; }

        [Display(Name = "Template Name")]
        public string LtmTname { get; set; } = string.Empty;

        [Display(Name = "Content")]
        
        public string LtmCntnt { get; set; } = string.Empty;

        [StringLength(200)]
        public string LtmUsrid { get; set; } = string.Empty;

        public DateTime LtmCdate { get; set; }

        public DateTime? LtmUdate { get; set; }
    }
}
