using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models.Eici_models
{
    public class DrugHistoryVM
    {
        public string ProductName { get; set; }
        public List<GoodsRecievedNoteLineVM> goodsRecievedNoteLineVMs { get; set; }
        public List<SaleHistory> saleHistories { get; set; }
        public List<PurchaseOrderLineVM> purchaseOrderLineVMs { get; set; }
    }
    public class GoodsRecievedNoteLineVM
    {
        public double Quantity { get; set; }
        public double Sold { get; set; }
        public double InStock { get; set; }
        public double Expired { get; set; }
        public string BatchID { get; set; }
        public string ExpiryDate { get; set; }
        public string GRNDate { get; set; }
        public string GoodsReceivedNoteName { get; set; }
        public string PurchaseOrdername { get; set; }
    }
    public class PurchaseOrderLineVM
    {
        public string PurchaseOrderName { get; set; }
        public string VendorName { get; set; }
        public double Quantity { get; set; }
        public double Total { get; set; }
        public string OrderDate { get; set; }
        public string DeliveryDate { get; set; }
    }
}
