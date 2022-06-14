using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class RFPSaleorder
    {
        public int RFPSaleorderId { get; set; }
        [Display(Name = "Order Number")]
        public string RFPSaleorderName { get; set; }
        [Display(Name = "Branch")]
        public int BranchId { get; set; }
        [Display(Name = "Customer")]
        public int RFPCustomerId { get; set; }
        public RFPCustomer RFPCustomer { get; set; }
        public RFPinvoice RFPinvoice { get; set; }
        public DateTimeOffset SaleDate { get; set; }
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }
        [Display(Name = "Sales Type")]
        public int SalesTypeId { get; set; }
        public string Remarks { get; set; }
        public double Amount { get; set; }
        public double SubTotal { get; set; }
        public double Discount { get; set; }
        public double Tax { get; set; }
        public double Freight { get; set; }
        public double Total { get; set; }
        public List<SalesOrderLine> SalesOrderLines { get; set; } = new List<SalesOrderLine>();
        public string UserId { get; set; }
    }
}
