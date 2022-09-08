using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class RFPRequestLine
    {
        public int RFPRequestLineId { get; set; }
        public int RFPRequestId { get; set; }
        public string Prescription { get; set; }
    }
}
