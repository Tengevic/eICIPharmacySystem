using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class GoodsRecievedNoteLine
    {
        public int GoodsRecievedNoteLineId { get; set; }
        [Display(Name = "GoodsReceivedNotedD")]
        public int GoodsReceivedNoteId { get; set; }
        [Display(Name = "GoodsReceivedNote ")]
        public GoodsReceivedNote GoodsReceivedNote { get; set; }
        [Display(Name = "Product Item")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public double Sold { get; set; }
        public double InStock { get; set; }
        public double Expired { get; set; }
        public string BatchID { get; set; }
        public DateTime ManufareDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Boolean Dispose { get; set; } = false;

    }
}
