using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models.Eici_models
{
    public class eiciPrescription
    {
        public string eiciNo { get; set; }
        public PatientDetail PatientDetail { get; set; }
        public List<PrescriptionDetail> prescription { get; set; }

    }
    public class PatientDetail
    {
        public string uuid { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string city { get; set; }
        public int type { get; set; }
    }
    public class PrescriptionDetail
    {
        public int OrderId { get; set; }
        public int? DrugId { get; set; }
        public string DrugName { get; set; }
        public string prescription { get; set; }
        public double Quantity { get; set; }
    }
}
