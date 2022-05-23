using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public string PrescriptionName { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime presciptionDate { get; set; }
        public bool Approved { get; set; } = false;
        public SalesOrder SalesOrder { get; set; }
        public ICollection<PrescriptionLines> prescriptionLines { get; set; }
    }
}
