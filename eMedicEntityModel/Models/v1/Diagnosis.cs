using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class Diagnosis
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int DiaAutid { get; set; }

        [Display(Name = "Name is required"), StringLength(100)]
        public string DiaSname { get; set; } = string.Empty;

        [Display(Name = "Category is required")]
        public int DiaCatid { get; set; }

        [ForeignKey("DiaCatid")]
        public Parameter? Category { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string DiaUsrid { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public bool DiaState { get; set; }

        public DateTime DiaCdate { get; set; }
        public DateTime? DiaUdate { get; set; }
    }

}
