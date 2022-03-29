using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class ClinicalTrialsStock
    {
        public int ClinicalTrialsStockId { get; set; }
        public int ClinicalTrialsProductsId { get; set; }
        public ClinicalTrialsProduct clinicalTrialsProducts { get; set; }
        public double TotalRecieved { get; set; }
        public double TotalSales { get; set; }
        public double InStock { get; set; }
        public double Deficit { get; set; }
    }
}
