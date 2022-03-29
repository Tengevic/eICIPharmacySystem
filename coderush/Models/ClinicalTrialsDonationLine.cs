using System;
using System.ComponentModel.DataAnnotations;

namespace coderush.Models
{
    public class ClinicalTrialsDonationLine
    {
        public int ClinicalTrialsDonationLineId { get; set; }
        [Display(Name = "ClinicalTrialsDonationId")]
        public int ClinicalTrialsDonationId { get; set; }
        [Display(Name = "ClinicalTrialsDonation ")]
        public ClinicalTrialsDonation clinicalTrialsDonation { get; set; }
        [Display(Name = "Product Item")]
        public int ClinicalTrialsProductsId { get; set; }
        public ClinicalTrialsProduct clinicalTrialsProducts { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public string BatchID { get; set; }
        public DateTime ManufareDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        //public bool isFullySold { get; set; }
    }
}