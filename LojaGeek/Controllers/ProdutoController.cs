using LojaGeek.Model.DB;
using LojaGeek.Model.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaGeek.Controllers
{
    public class ProdutoController : Controller
    {
        public ActionResult Index()
        {
            var produtos = DbFactory.Instance.ProdutoRepository.FindAll();

            return View(produtos);
        }

        public ActionResult CadastrarProduto()
        {
            return View("EditarProduto", new Produto());
        }

        public ActionResult GravarProduto(Produto produto)
        {
            DbFactory.Instance.ProdutoRepository.SaveOrUpdate(produto);
            return RedirectToAction("Index");
        }

        public ActionResult ApagarProduto(Guid id)
        {
            var produto = DbFactory.Instance.ProdutoRepository.FindById(id);

            if (produto != null)
            {
                DbFactory.Instance.ProdutoRepository.Delete(produto);
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditarProduto(Guid id)
        {
            var produto = DbFactory.Instance.ProdutoRepository.FindById(id);

            if (produto != null)
            {
                return View(produto);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Detalhes(Guid id)
        {
            ViewBag.produto = DbFactory.Instance.ProdutoRepository.FindById(id);
            ViewBag.comentarios = DbFactory.Instance.ComentarioRepository.FindAll();
            return View();
        }

        public ActionResult BuscarPeloNome(String edtBusca)
        {
            if (String.IsNullOrEmpty(edtBusca))
            {
                return RedirectToAction("Index");
            }

            var produtos = DbFactory.Instance.ProdutoRepository.GetAllByName(edtBusca);

            return View("Index", produtos);
        }
    }
}