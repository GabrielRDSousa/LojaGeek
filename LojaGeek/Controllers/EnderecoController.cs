using LojaGeek.Model.DB;
using LojaGeek.Model.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaGeek.Controllers
{
    public class EnderecoController : Controller
    {
        // GET: Endereco
        public ActionResult Index(String escolha)
        {
            Cliente usuario = (Cliente)Session["usuario"];
            var enderecos = DbFactory.Instance.EnderecoRepository.GetAllByCliente(usuario);
            ViewBag.Escolha = escolha;
            ViewBag.enderecos = enderecos;
            return View();
        }
        public ActionResult CadastrarEndereco(Endereco endereco, String e)
        {
            Cliente usuario = (Cliente)Session["usuario"];
            endereco.Cliente = usuario;
            DbFactory.Instance.EnderecoRepository.SaveOrUpdate(endereco);
            return RedirectToAction("Index", new { escolha = e});
        }
    }
}