using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class RFPRequest
    {
        public int RFPRequestId { get; set; }
        public string RFPRquestName { get; set; }
        public DateTimeOffset RequestDate { get; set; }
        public string FundingApplicationGuid { get; set; }
        public int RFPCustomerId { get; set; }
        public RFPCustomer RFPCustomer { get; set; }
        public int InstitutionId { get; set; }
        public string InstitutionName { get; set; }
        public string userId { get; set; }
    }
}
