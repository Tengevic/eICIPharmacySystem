using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class ClinicalTrialsProduct
    {
        public int ClinicalTrialsProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        public string ProductCode { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public string ProductImageUrl { get; set; }
        [Display(Name = "UOM")]
        public int UnitOfMeasureId { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        [Display(Name = "Branch")]
        public int BranchId { get; set; }
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }
        public double TotalRecieved { get; set; }
        public double TotalSales { get; set; }
        public double InStock { get; set; }
        public double Deficit { get; set; }
        public double Returned { get; set; }
        public double Expired { get; set; }
    }
}
