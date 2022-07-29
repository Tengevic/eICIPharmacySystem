using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models.Eici_models
{
    public class CustomerSaleOrderLineVM
    {
        public string ProductName { get; set; }
        public string BatchNo { get; set; }
        public double Quantity { get; set; }
        public string SalesOrderName { get; set; }
        public double Total { get; set; }
        public string saledate { get; set; }
        public string InvoceName { get; set; }
        public string PaymentReciveName { get; set; }
        public string PaymentType { get; set; }

    }
}
