﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using coderush.Data;
using coderush.Models;
using coderush.Services;
using coderush.Models.SyncfusionViewModels;
using Microsoft.AspNetCore.Authorization;
using coderush.Models.Eici_models;
using Newtonsoft.Json;

namespace coderush.Controllers.Api
{

    [Produces("application/json")]
    [Route("api/Prescription")]
    public class PrescriptionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public PrescriptionController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }
        [HttpGet]
        public async Task<IActionResult> GetPrescription()
        {
            List<Prescription> Items = await _context.Prescription.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
        //api/Prescription/GetNotSoldYet
        [HttpGet("[action]")]
        public async Task<IActionResult> GetNotSoldYet()
        {
            List<Prescription> prescription = new List<Prescription>();
            try
            {
                List<SalesOrder> salesOrder = new List<SalesOrder>();
                salesOrder = await _context.SalesOrder.ToListAsync();
                List<int?> ids = new List<int?>();

                foreach (var item in salesOrder)
                {
                    ids.Add(item.PrescriptionId);
                }

                prescription = await _context.Prescription
                    .Where(x => !ids.Contains(x.PrescriptionId))
                    .Where(x => x.Approved == true)
                    .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(prescription);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Prescription result = await _context.Prescription
                .Where(x => x.PrescriptionId.Equals(id))
                .Include(x => x.prescriptionLines)
                .FirstOrDefaultAsync();

            return Ok(result);
        }
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody] CrudViewModel<Prescription> payload)
        {
            Prescription prescription = payload.value;
            prescription.PrescriptionName = _numberSequence.GetNumberSequence("PS");
            _context.Prescription.Add(prescription);
            _context.SaveChanges();        
            return Ok(prescription);
        }
        //Endpoint
        [HttpPost("[action]")]
        public IActionResult Add([FromBody] eiciPrescription payload)
        {
            eiciPrescription eiciPrescription = payload;
            if(eiciPrescription.prescription.Count == 0)
            {
                Err err = new Err
                {
                    message = "No prescription"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }

            Customer customer = _context.Customer.
                Where(x => x.EiciRefNumber == eiciPrescription.eiciNo)
                .FirstOrDefault();
            if (customer == null)
            {
                customer = new Customer {
                    EiciRefNumber = eiciPrescription.eiciNo,
                    CustomerName = eiciPrescription.PatientDetail.Name,
                    Address = eiciPrescription.PatientDetail.Address,
                    City = eiciPrescription.PatientDetail.city,
                    CustomerTypeId = eiciPrescription.PatientDetail.type,
                    Phone = eiciPrescription.PatientDetail.PhoneNumber
                };
                _context.Customer.Add(customer);
                _context.SaveChanges();
            }
            Prescription prescription = new Prescription
            {
                CustomerId = customer.CustomerId,
                presciptionDate= DateTime.Now,
                Approved = false
            };
            prescription.PrescriptionName = _numberSequence.GetNumberSequence("PS");
            _context.Prescription.Add(prescription);
            _context.SaveChanges();
            foreach (PrescriptionDetail prescriptionDetail in eiciPrescription.prescription) 
            {
                PrescriptionLines prescriptionLines = new PrescriptionLines
                {
                    ProductId = prescriptionDetail.DrugId,
                    Quantity = prescriptionDetail.Quantity,
                    PrescriptionId = prescription.PrescriptionId,
                    OderId = prescriptionDetail.OrderId,
                    prescription = prescriptionDetail.prescription
                };
                prescriptionLines.PrescriptionLinesName = _numberSequence.GetNumberSequence("PSL");
                _context.PrescriptionLines.Add(prescriptionLines);
                _context.SaveChanges();
            } 
            
            
            
            return Ok(prescription);
        }
        [HttpPost("[action]")]
        public IActionResult Update([FromBody] CrudViewModel<Prescription> payload)
        {
            Prescription prescription = payload.value;
            _context.Prescription.Update(prescription);
            _context.SaveChanges();
            return Ok(prescription);
        }
        [HttpPost("[action]")]
        public IActionResult Put([FromBody] Prescription payload)
        {
            Prescription prescription = payload;
            _context.Prescription.Update(prescription);
            _context.SaveChanges();
            return Ok(prescription);
        }
        [HttpPost("[action]")]
        public IActionResult Approve([FromBody] Prescription payload)
        {
            Prescription prescription = payload;
            Prescription Update = _context.Prescription.Find(prescription.PrescriptionId);
            Update.Approved = prescription.Approved;
            _context.Prescription.Update(Update);
            _context.SaveChanges();
            return Ok(prescription);
        }
        [HttpPost("[action]")]
        public IActionResult Remove([FromBody] CrudViewModel<Prescription> payload)
        {
            Prescription prescription = _context.Prescription
                .Where(x => x.PrescriptionId == (int)payload.key)
                .FirstOrDefault();
            _context.Prescription.Remove(prescription);
            _context.SaveChanges();
            return Ok(prescription);

        }
        [HttpPost("[action]/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            Prescription prescription = _context.Prescription
                 .Where(x => x.PrescriptionId == id)
                 .FirstOrDefault();
            _context.Prescription.Remove(prescription);
            _context.SaveChanges();
            return Ok(prescription);

        }
    }
}
