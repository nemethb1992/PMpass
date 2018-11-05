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
            if(model.Username != null && model.OldPasswrod != null && model.NewPassword1 != null && model.NewPassword2 != null)
            {
                if (model.NewPassword1 == model.NewPassword2)
                {
                    ViewData["Message"] = new Password().Reset(model.Username, model.OldPasswrod, model.NewPassword1, model.NewPassword2);
                }
                else
                {
                    ViewData["Message"] = "Új jelszó nem egyezik!";
                }
            }
            else
            {
                ViewData["Message"] = "";
            }
            return View();
        }
    }
}
