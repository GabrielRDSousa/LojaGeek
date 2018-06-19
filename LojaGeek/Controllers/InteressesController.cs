using LojaGeek.Model.DB;
using LojaGeek.Model.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaGeek.Controllers
{
    public class InteressesController : Controller
    {
        public ActionResult CadastrarInteresses()
        {
            return View();
        }

        public ActionResult SalvarInteresse(Interesse interesse)
        {

            if (ModelState.IsValid)
            {
                DbFactory.Instance.InteresseRepository.SaveOrUpdate(interesse);
                return RedirectToAction("EntrarCliente", "Cliente");
            }
            else
            {
                return View("EntrarCliente", interesse);
            }

        }
    }
}