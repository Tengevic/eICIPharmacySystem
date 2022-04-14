using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class ClinicalTrialsDonation //clinical trials recieve
    {
        public int ClinicalTrialsDonationId { get; set; }
        [Display(Name = "CTD Number")]
        public string ClinicalTrialsDonationName { get; set; }
        [Display(Name = "CTD Date")]
        public DateTimeOffset CTDDate { get; set; }
        [Display(Name = "Vendor Delivery Order #")]
        public string VendorDONumber { get; set; }
        [Display(Name = "Vendor")]
        public int VendorId { get; set; }
        [Display(Name = "Warehouse")]
        public Vendor Vendor { get; set; }
        public int WarehouseId { get; set; }
   
        public List<ClinicalTrialsDonationLine> clinicalTrialsDonationLine { get; set; } = new List<ClinicalTrialsDonationLine>();
    }
}

