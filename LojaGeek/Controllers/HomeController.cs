using LojaGeek.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaGeek.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var esportes = DbFactory.Instance.ClienteRepository.FindAll();
            var produtos = DbFactory.Instance.ProdutoRepository.FindAll();
            ViewBag.produtos = produtos;
            return View();
        }

        public ActionResult Produto(Guid id)
        {
            return RedirectToAction("Detalhes","Produto", new { id=id });
        }
    }
}