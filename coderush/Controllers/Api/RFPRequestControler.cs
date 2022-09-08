using coderush.Data;
using coderush.Models;
using coderush.Models.AtMViewModels;
using coderush.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/RFPRequest")]
    public class RFPRequestControler : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public RFPRequestControler(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
    
        }
        [HttpGet]
        public async Task<IActionResult> GetRFPRequest()
        {
            List<RFPRequest> Items = await _context.RFPRequest
                .OrderByDescending(x => x.RFPRequestId)
                .ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody] PharmacyRequest payload)
        {
            PharmacyRequest pharmacyRequest = payload;
            RFPCustomer customer =  await _context.RFPCustomer.FirstOrDefaultAsync(x => x.PatientGuid == pharmacyRequest.PatientGuid);
            if(customer == null)
            {
                RFPCustomer newCustomer = new RFPCustomer
                {
                    PatientGuid = pharmacyRequest.PatientGuid,
                    CustomerTypeId = 1,
                    RFPCustomerName = pharmacyRequest.Name,
                    City = pharmacyRequest.Location,                
                };
                _context.RFPCustomer.Add(newCustomer);
                _context.SaveChanges();
                customer = newCustomer;
            }
            RFPRequest request = new RFPRequest{
                FundingApplicationGuid = pharmacyRequest.FundingApplicationGuid,
                InstitutionId = pharmacyRequest.InstitutionId,
                InstitutionName = pharmacyRequest.InstitutionName,
                RequestDate = DateTime.Now,
                RFPCustomerId = customer.RFPCustomerId,
            };
            request.RFPRquestName = _numberSequence.GetNumberSequence("RRN");

            _context.RFPRequest.Add(request);
            _context.SaveChanges();
            
            foreach(PrescribedRegimin prescribedRegimin in pharmacyRequest.Prescriptions)
            {
                RFPRequestLine requestLine = new RFPRequestLine {
                    Prescription = prescribedRegimin.Prescription,
                    RFPRequestId = request.RFPRequestId
                };
                _context.RFPRequestLine.Add(requestLine);
                _context.SaveChanges();
            }
            pharmacyRequest.Completed = true;
            return Ok(pharmacyRequest);
        }
    }
}
