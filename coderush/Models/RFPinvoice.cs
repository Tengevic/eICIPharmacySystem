using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class RFPinvoice
    {
        public int RFPinvoiceId { get; set; }
        [Display(Name = "Invoice Number")]
        public string RFPinvoiceName { get; set; }
        //  [Display(Name = "Shipment")]
        //  public int ShipmentId { get; set; }
        public int RFPSaleorderId { get; set; }
        [Display(Name = "Invoice Date")]
        public RFPSaleorder RFPSaleorder { get; set; }
        public DateTimeOffset InvoiceDate { get; set; }
        [Display(Name = "Invoice Due Date")]
        public DateTimeOffset InvoiceDueDate { get; set; }
        [Display(Name = "Invoice Type")]
        public int InvoiceTypeId { get; set; }
        public bool fullyPaid { get; set; } = false;
        public RFPpaymentRecieved RFPpaymentRecieved { get; set; }
        public string UserId { get; set; }
    }
}
