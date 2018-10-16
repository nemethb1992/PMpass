using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PMpass.Models;
using PMpass.Utility;

namespace PMpass.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(CredentialData model)
        {
            if(model != null)
            {
                Password password = new Password();
                ViewData["Message"] = password.Reset(model.Username, model.OldPasswrod, model.NewPassword1, model.NewPassword2);
            }
            return View();
        }
    }
}
