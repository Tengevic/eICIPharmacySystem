using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models.AtMViewModels
{
    public class PharmacyRequest
    {
        public string FundingApplicationGuid { get; set; }
        public int InstitutionId { get; set; }
        public string InstitutionName { get; set; }
        public string Location { get; set; }
        public string PatientGuid { get; set; }
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public bool Completed { get; set; } 
        public List<PrescribedRegimin> Prescriptions { get; set; }
    }
    public class PrescribedRegimin
    {
        public string Prescription { get; set; }
    }
}