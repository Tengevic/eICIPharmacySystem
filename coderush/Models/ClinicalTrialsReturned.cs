using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class ClinicalTrialsReturned
    {
        public int ClinicalTrialsReturnedId { get; set; }
        [Display(Name = "Clinical Trail sale Number")]
        public string ClinicalTrialsReturnedName { get; set; }
        //[Display(Name = "Branch")]
        //public int BranchId { get; set; }
        [Display(Name = "Vendor")]
        public int VendorId { get; set; }
        public DateTimeOffset ReturnedDate { get; set; }
        public List<ClinicalTrialsReturnedLine> ClinicalTrialsReturnedLines { get; set; } = new List<ClinicalTrialsReturnedLine>();

    }
}
