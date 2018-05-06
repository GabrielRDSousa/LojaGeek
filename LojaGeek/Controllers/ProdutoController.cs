using LojaGeek.Model.DB;
using LojaGeek.Model.DB.Model;
using LojaGeek.Model.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaGeek.Controllers
{
    public class ProdutoController : Controller
    {

        public ActionResult CadastrarProduto()
        {
            return View();
        }

        public ActionResult GravarProduto(Produto produto)
        {
            var precoFabrica = produto.Preco;
            var precoFinal = CalculoUtils.CalcularPrecoReal(precoFabrica);
            produto.Preco = precoFinal;

            DbFactory.Instance.ProdutoRepository.SaveOrUpdate(produto);
            return RedirectToAction("Estoque", "Administrativo") ;
        }


        public ActionResult Detalhes(Guid id)
        {
            var produto = DbFactory.Instance.ProdutoRepository.FindById(id);
            return View(produto);
        }
    }
}