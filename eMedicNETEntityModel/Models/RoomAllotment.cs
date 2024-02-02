﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicNETEntityModel.Models
{
    public class RoomAllotment
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int RalAutid { get; set; }

        [Display(Name = "Room No")]
        [Required]
        public int RalRomid { get; set; }

        [ForeignKey("RalRomid")]
        public Room Room { get; set; } = null!;

        [Display(Name = "Patient")]
        [Required]
        public int RalPatid { get; set; }

        [ForeignKey("RalPatid")]
        public Patient Patient { get; set; } = null!;

        [StringLength(200)]
        public string RalUsrid { get; set; } = null!;

        public DateTime RalCdate { get; set; }

        public DateTime RalUdate { get; set; }
    }
}
