using System.ComponentModel.DataAnnotations;

namespace coderush.Models
{
    public class ClinicalTrialsReturnedLine
    {
        public int ClinicalTrialsReturnedLineId { get; set; }
        [Display(Name = "Sales Order")]
        public int ClinicalTrialsReturnedId { get; set; }
        [Display(Name = "Sales Order")]
        public ClinicalTrialsReturned clinicalTrialsReturned { get; set; }
        [Display(Name = "Drug Item")]
        public int ClinicalTrialsProductsId { get; set; }
        public int ClinicalTrialsDonationLineId { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
      
    }
}