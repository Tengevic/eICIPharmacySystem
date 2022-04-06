using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Controllers
{
    [Authorize(Roles = Pages.MainMenu.ClinicalTrialsNotifications.RoleName)]
    public class ClinicalTrialsNotifications : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
