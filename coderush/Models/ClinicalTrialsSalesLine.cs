﻿using System.ComponentModel.DataAnnotations;

namespace coderush.Models
{
    public class ClinicalTrialsSalesLine
    {
        public int ClinicalTrialsSalesLineId { get; set; }
        [Display(Name = "Sales Order")]
        public int ClinicalTrialsSalesId { get; set; }
        [Display(Name = "Sales Order")]
        public ClinicalTrialsSales clinicalTrialsSales { get; set; }
        [Display(Name = "Product Item")]
        public int ClinicalTrialsProductsId { get; set; }
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
    }
}