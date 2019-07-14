using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Session14.Validations.Infra;
using System;
using System.ComponentModel.DataAnnotations;

namespace Session14.Validations.Models
{
    public class Appointment
    {        
        public string ClientName { get; set; }

        [NationalId]
        public string NationalId { get; set; }

        [UIHint("Date")]
        public DateTime Date { get; set; }

        [MustBeTrue(true, "Make it True..")]
        public bool TermsAccpted { get; set; }
                
        [FileSize(10, 10 * 1024 * 1024)]
        [FileType(" .JpG , .Bmp , .pnG ")]
        public IFormFile Image { get; set; }
    }
}
