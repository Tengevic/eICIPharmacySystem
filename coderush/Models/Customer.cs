﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Display(Name = "Customer Type")]
        public int CustomerTypeId { get; set; }
        public CustomerType customerType { get; set; }
        [Display(Name = "NHIF")]
        public string Address { get; set; }// stores NHIF detalis not address
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string EiciRefNumber { get; set; }
    }
}
