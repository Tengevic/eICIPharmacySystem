using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class ClinicalTrialsDonation
    {
        public int ClinicalTrialsDonationId { get; set; }
        [Display(Name = "CTD Number")]
        public string ClinicalTrialsDonationName { get; set; }
        [Display(Name = "CTD Date")]
        public DateTimeOffset CTDDate { get; set; }
        [Display(Name = "Vendor Delivery Order #")]
        public string VendorDONumber { get; set; }
        [Display(Name = "Vendor Bill / Invoice #")]
        public string VendorInvoiceNumber { get; set; }
        [Display(Name = "Warehouse")]
        public int WarehouseId { get; set; }
   
        public List<ClinicalTrialsDonationLine> clinicalTrialsDonationLine { get; set; } = new List<ClinicalTrialsDonationLine>();
    }
}

