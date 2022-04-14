using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class ClinicalTrialsSales // use
    {
        public int ClinicalTrialsSalesId { get; set; }
        [Display(Name = "Clinical Trail sale Number")]
        public string ClinicalTrialsSalesName { get; set; }
        [Display(Name = "patient")]
        public int CustomerId { get; set; }
        public DateTimeOffset UseDate { get; set; }
        [Display(Name = "Patient Ref. Number")]
        public string PatientRefNumber { get; set; }
        public List<ClinicalTrialsSalesLine> clinicalTrialsSalesLine { get; set; } = new List<ClinicalTrialsSalesLine>();
    }
}
