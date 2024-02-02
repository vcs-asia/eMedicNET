﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eMedicEntityModel.Models.v1
{
    public class StockGoodsReceiveNoteDetail
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int GsdAutid { get; set; }

        [Display(Name = "Grn ID")]
        public int GsdGrnid { get; set; }

        [ForeignKey("GsdGrnid")]
        public StockGoodsReceiveNote? StockGoodsReceiveNote { get; set; }

        [Display(Name = "Stock ID")]
        public int GsdStkid { get; set; }

        [ForeignKey("GsdStkid")]
        public Product? Product { get; set; }

        [Display(Name = "Packing")]
        public int GsdIpack { get; set; }

        [Display(Name = "Received Quantity")]
        public int GsdRcqty { get; set; }

        [Display(Name = "Cost")]
        public decimal GsdScost { get; set; }

        [Display(Name = "Amount")]
        public decimal GsdAmont { get; set; }

        [Display(Name = "GST Percent")]
        public decimal GsdGstvl { get; set; }

        [Display(Name = "GST")]
        public decimal GsdGstam { get; set; }

        [Display(Name = "Batch No")]
        public string GsdBatno { get; set; } = string.Empty;

        [Display(Name = "Batch ID")]
        public int GsdBatid { get; set; }

        [Display(Name = "Exp. Date")]
        public DateTime GsdExpdt { get; set; }

        [Display(Name = "Exp. Flag")]
        public bool GsdExpfl { get; set; }

        [Display(Name = "PO ID")]
        public int GsdTopid { get; set; }

        [ForeignKey("GsdTopid")]
        public StockPurchaseOrderDetail? PurchaseOrderDetail { get; set; }

        [Display(Name = "User ID"), StringLength(150)]
        public string GsdUsrid { get; set; } = string.Empty;

        public DateTime GsdCdate { get; set; }
        public DateTime? GsdUdate { get; set; }
    }

}
