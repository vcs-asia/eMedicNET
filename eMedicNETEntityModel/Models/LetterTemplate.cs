using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class LetterTemplate
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int LtmAutid { get; set; }

        [Display(Name = "Content")]
        [Required]
        public string LtmCntnt { get; set; } = null!;

        [StringLength(200)]
        public string LtmUsrid { get; set; } = null!;

        public DateTime LtmCdate { get; set; }

        public DateTime LtmUdate { get; set; }
    }
}
