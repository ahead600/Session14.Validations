using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Session14.Validations.Models;

namespace Session14.Validations.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [RequestSizeLimit(1 * 1024 * 1024)]
        public IActionResult MakeBooking(Appointment appt)
        {            
            if (string.IsNullOrEmpty(appt.ClientName))
            {
                var a = appt.ClientName;
                var b = nameof(appt.ClientName);
                ModelState.AddModelError(nameof(appt.ClientName), "Please enter your name..");
            }

            if (ModelState.GetValidationState("Date")==ModelValidationState.Valid &&
                DateTime.Now>appt.Date)
            {
                ModelState.AddModelError(nameof(appt.Date), "Please Enter Future date..");
            }
            if (ModelState.IsValid)
            {
                return View("Completed", appt);
            }
            return View("Index");
            
        }        
    }
}