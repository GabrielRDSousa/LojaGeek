using LojaGeek.Model.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaGeek.Controllers
{
    public class AdministrativoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginAdministrativo(string senha)
        {
            if (LoginUtils.LoginAdministrativo(senha))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}