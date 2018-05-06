using LojaGeek.Model.DB;
using LojaGeek.Model.DB.Model;
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
            var temCarrinho = Session["carrinho"];
            if (temCarrinho == null)
            {
                Carrinho carrinho = new Carrinho();
                Session["sid"] = Session.SessionID;
                carrinho.Sessao = (String)Session["sid"];
                carrinho = DbFactory.Instance.CarrinhoRepository.SaveOrUpdate(carrinho);
                Session["carrinho"] = carrinho;
            }
            
            var produtosAux = DbFactory.Instance.ProdutoRepository.FindAll();
            var produtos = new List<Produto>();
            foreach (Produto produto in produtosAux)
            {
                if (produto.Ativo)
                {
                    produtos.Add(produto);
                }
            }
            return View(produtos);
        }

        public ActionResult DetalheProduto(Guid id)
        {
            return RedirectToAction("Detalhes","Produto", new { id=id });
        }
    }
}