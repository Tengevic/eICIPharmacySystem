using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models.Eici_models
{
    public class stockNumberVM
    {
        public int stockNumberId { get; set; }
        public int GoodsRecievedNoteLineId { get; set; }
        public string Username { get; set; }
        public DateTime date { get; set; }
        public double Add { get; set; }
        public double subtract { get; set; }
        public string Description { get; set; }
        public double quantity { get; set; }
    }
}
