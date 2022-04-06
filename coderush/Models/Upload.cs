using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{

    public class Upload
    {
        public int Id { get; set; }
        public string filename { get; set; }
        public int PaymentReceiveId { get; set; }
        public byte[] Content { get; set; }
    }
}
