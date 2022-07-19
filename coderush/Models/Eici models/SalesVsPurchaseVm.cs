using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models.Eici_models
{
    public class SalesVsPurchaseVm
    {
        public string ProductName { get; set; }
        public double TotalUnitsSold { get; set; }
        public double TotalSales { get; set; }
        public double TotalUnitsPurchased { get; set; }
        public double TotalUnitsRecieved { get; set; }
        public double TotalPurchase { get; set; }
        public double Profit { get; set; }
        public double RemainderStock { get; set; }
    }
}
