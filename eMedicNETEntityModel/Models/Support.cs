using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace eMedicNETEntityModel.Models
{
    public class Support
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int SrtAutid { get; set; }

        [Display(Name = "User ID"), StringLength(128), Required(ErrorMessage = "{0} is required")]
        public string SrtUsrid { get; set; } = null!;

        [StringLength(150)]
        public string SrtEmail { get; set; } = null!;

        [StringLength(20)]
        public string SrtSubjt { get; set; } = null!;

        [StringLength(1000)]
        public string SrtMessg { get; set; } = null!;

        public bool SrtPflag { get; set; }

        [StringLength(200)]
        public string SrtFpath { get; set; } = null!;

        [NotMapped]
        public IFormFile SrtFfile { get; set; } = null!;

        public DateTime SrtCdate { get; set; }
        public DateTime SrtUdate { get; set; }
    }

}
