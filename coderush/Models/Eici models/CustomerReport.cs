using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models.Eici_models
{
    public class CustomerReport
    {
        public string CustomerName { get; set; }
        public Customer Customer { get; set; }
        public List<ProductUsage> productUsage { get; set; }
        public List<CustomerSaleOrderLineVM> salesOrderLines { get; set; }
    }

    public class ProductUsage
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
    }
}
