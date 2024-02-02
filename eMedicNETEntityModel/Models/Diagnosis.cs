using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class Diagnosis
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int DiaAutid { get; set; }

        [Display(Name = "Name is required"), StringLength(100)]
        public string DiaSname { get; set; } = null!;

        [Display(Name = "Category is required"), Required(ErrorMessage = "{0} is required")]
        public int DiaCatid { get; set; }

        [ForeignKey("DiaCatid")]
        public Parameter Category { get; set; } = null!;

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string DiaUsrid { get; set; } = null!;

        [Display(Name = "Status")]
        public bool DiaState { get; set; }

        public DateTime DiaCdate { get; set; }
        public DateTime DiaUdate { get; set; }
    }

}
