﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class RFPCustomer
    {
        public int RFPCustomerId { get; set; }
        [Required]
        public string RFPCustomerName { get; set; }
        [Display(Name = "Customer Type")]
        public int CustomerTypeId { get; set; }
        [Display(Name = "Street Address")]
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
