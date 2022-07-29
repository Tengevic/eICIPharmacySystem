﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models.Eici_models
{
    public class saleOrderlineVM
    {
        public int SalesOrderLineId { get; set; }
        [Display(Name = "Sales Order")]
        public int? SalesOrderId { get; set; }
        [Display(Name = "Sales Order")]
        public SalesOrder SalesOrder { get; set; }
        public int? RFPSaleorderId { get; set; }
        public RFPSaleorder RFPSaleorder { get; set; }
        [Display(Name = "Product Item")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int GoodsRecievedNoteLineId { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        [Display(Name = "Disc %")]
        public double DiscountPercentage { get; set; }
        public double DiscountAmount { get; set; }
        public double SubTotal { get; set; }
        [Display(Name = "Tax %")]
        public double TaxPercentage { get; set; }
        public double TaxAmount { get; set; }
        public double Total { get; set; }
        public int? PrescriptionLinesId { get; set; }
    }
}