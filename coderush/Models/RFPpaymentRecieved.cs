using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class RFPpaymentRecieved
    {
        public int RFPpaymentRecievedId { get; set; }
        [Display(Name = "Payment Number")]
        public string RFPpaymentRecievedName { get; set; }
        [Display(Name = "Invoice")]
        public int RFPinvoiceId { get; set; }
        public RFPinvoice RFPinvoice { get; set; }
        public DateTimeOffset PaymentDate { get; set; }
        [Display(Name = "Payment Type")]
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
        public double PaymentAmount { get; set; }
        [Display(Name = "Full Payment")]
        public bool IsFullPayment { get; set; } = true;
        public RFPDrugRecieve RFPDrugRecieve { get; set; }
    }
}
