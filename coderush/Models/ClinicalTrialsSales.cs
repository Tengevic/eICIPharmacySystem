using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class ClinicalTrialsSales
    {
        public int ClinicalTrialsSalesId { get; set; }
        [Display(Name = "Clinical Trail sale Number")]
        public string ClinicalTrialsSalesName { get; set; }
        //[Display(Name = "Branch")]
        //public int BranchId { get; set; }
        [Display(Name = "patient")]
        public int CustomerId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }

        //[Display(Name = "Currency")]
        //public int CurrencyId { get; set; }

        [Display(Name = "Patient Ref. Number")]
        public string PatientRefNumber { get; set; }
        //[Display(Name = "Sales Type")]
        //public int SalesTypeId { get; set; }
        //public string Remarks { get; set; }
        //public double Amount { get; set; }
        //public double SubTotal { get; set; }
        //public double Discount { get; set; }
        //public double Tax { get; set; }
        //public double Freight { get; set; }
        //public double Total { get; set; }
        public List<ClinicalTrialsSalesLine> clinicalTrialsSalesLine { get; set; } = new List<ClinicalTrialsSalesLine>();
    }
}
