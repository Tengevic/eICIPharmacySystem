using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models.AtMViewModels
{
    public class ProductAtMVM
    {
        public string ProductName { get; set; }
        public string UnitOfMeasureName { get; set; }
        public string BrandName { get; set; }
        public double DefaultSellingPrice { get; set; } = 0.0;
        public double InStock { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
