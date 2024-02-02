using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class Parameter
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int PrmAutid { get; set; }

        [Display(Name = "Description"), StringLength(100), Required(ErrorMessage = "{0} is required")]
        public string PrmPdesc { get; set; } = null!;

        [Display(Name = "Type"), Required(ErrorMessage = "{0} is required")]
        [StringLength(100)]
        public string PrmPtype { get; set; } = null!;

        [Display(Name = "Status")]
        public bool PrmState { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "{0} is required"), StringLength(150)]
        public string PrmUsrid { get; set; } = null!;

        public DateTime PrmCdate { get; set; }
        public DateTime PrmUdate { get; set; }
    }

}
