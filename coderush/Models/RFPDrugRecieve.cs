using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class RFPDrugRecieve
    {
        public int RFPDrugRecieveId { get; set; }
        [Display(Name = "GRN Number")]
        public string RFPDrugRecieveName { get; set; }
        public int RFPpaymentRecievedId { get; set; }
        public RFPpaymentRecieved  RFPpaymentRecieved { get; set;}
        [Display(Name = "GRN Date")]
        public DateTimeOffset GRNDate { get; set; }
        [Display(Name = "Full Receive")]
        public bool IsFullReceive { get; set; } = true;
        public List<GoodsRecievedNoteLine> goodsRecievedNoteLines { get; set; } = new List<GoodsRecievedNoteLine>();
        public string UserId { get; set; }
    }
}

