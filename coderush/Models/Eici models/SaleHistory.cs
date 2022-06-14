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
        public string CustomerName { get; set; }
        public double Total { get; set; }
        public DateTimeOffset saledate { get; set; }
        
    }
}
