using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models.Eici_models
{
    public class SaleHistory
    {
        public string ProductName { get; set; }
        public double Quanity { get; set; }
        public string SaleOrderName { get; set; }
        public string saledate { get; set; }
        public string CustomerName { get; set; }
        public string NHIF { get; set; }
        public string EiciRefNumber { get; set; }
        public double Total { get; set; }
        public string PaymentMode { get; set; }
        public string BatchNumber { get; set; }
        public string ExpiryDate { get; set; }
            
    }
}
