using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class Stock
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public double TotalRecieved { get; set; }
        public double TotalSales { get; set; }
        public double InStock { get; set; }
        public double Deficit { get; set; }
    }
}
