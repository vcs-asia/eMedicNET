using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace eMedicEntityModel.Models.v1
{
    public class Support
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int SrtAutid { get; set; }

        [Display(Name = "User ID"), StringLength(128)]
        public string SrtUsrid { get; set; } = string.Empty;

        [StringLength(150)]
        public string SrtEmail { get; set; } = string.Empty;

        [StringLength(20)]
        public string? SrtSubjt { get; set; }

        [StringLength(1000)]
        public string? SrtMessg { get; set; }

        public bool SrtPflag { get; set; }

        [StringLength(200)]
        public string SrtFpath { get; set; } = string.Empty;

        [NotMapped]
        public IFormFile? SrtFfile { get; set; }

        public DateTime SrtCdate { get; set; }
        public DateTime SrtUdate { get; set; }
    }

}
