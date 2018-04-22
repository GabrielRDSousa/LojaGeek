using LojaGeek.Model.DB;
using LojaGeek.Model.DB.Model;
using LojaGeek.Model.DB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaGeek.Controllers
{

    public class CarrinhoController : Controller
    {
        public ActionResult Index()
        {
            string sessionId = this.Session.SessionID;

            var carrinho = DbFactory.Instance.CarrinhoRepository.GetAllBySession(sessionId); ;
            ViewBag.Carrinho = carrinho;

            var total = 0.0;
            foreach (var item in carrinho)
            {
                total = total + (item.Produto.PrecoUnitario * item.Quantidade);
            }

            var valorCarrinho = DbFactory.Instance.ValorCarrinhoRepository.FindByIdString(sessionId);
            if (valorCarrinho.Cupom != null) { total = total - (total * 0.1); };

            ValorCarrinho valor = new ValorCarrinho();
            valor.Total = total;
            valor.Id = sessionId;
            valor.Cupom = valorCarrinho.Cupom;
            var valores = DbFactory.Instance.ValorCarrinhoRepository.SaveOrUpdate(valor);
            ViewBag.Valores = valores;
            
            return View();
        }
        public ActionResult ColocarCarrinho(Guid id_produto, int quantidade)
        {
            Produto produto = DbFactory.Instance.ProdutoRepository.FindById(id_produto);
            Carrinho carrinho = new Carrinho();
            if (this.Session["Usuario"] != null)
            {
                Cliente cliente = (Cliente) this.Session["Usuario"];
                carrinho.Cliente = cliente;
            }            
            string sessionId = this.Session.SessionID;
            carrinho.Produto = produto;
            carrinho.Quantidade = quantidade;
            carrinho.Sessao = sessionId;
            DbFactory.Instance.CarrinhoRepository.SaveOrUpdate(carrinho);
            
            return RedirectToAction("Index");
        }

        public ActionResult RetirarDoCarrinho(Guid id)
        {
            string sessionId = this.Session.SessionID;
            Carrinho carrinho = DbFactory.Instance.CarrinhoRepository.FindById(id);
            var valorCarrinho = DbFactory.Instance.ValorCarrinhoRepository.FindByIdString(sessionId);
            valorCarrinho.Total = valorCarrinho.Total - carrinho.Produto.PrecoUnitario;
            DbFactory.Instance.ValorCarrinhoRepository.SaveOrUpdate(valorCarrinho);

            DbFactory.Instance.CarrinhoRepository.Delete(carrinho);

            return RedirectToAction("Index");
        }

        public ActionResult Comprar()
        {
            string sessionId = this.Session.SessionID;
            var carrinho = DbFactory.Instance.CarrinhoRepository.GetAllBySession(sessionId); ;
            ViewBag.Carrinho = carrinho;
            return View();
        }

        public ActionResult AtualizarTotal(Guid id_item_carrinho, int quantidade)
        {
            var carrinho = DbFactory.Instance.CarrinhoRepository.FindById(id_item_carrinho);
            carrinho.Quantidade = quantidade;
            DbFactory.Instance.CarrinhoRepository.SaveOrUpdate(carrinho);

            string sessionId = this.Session.SessionID;
            var valorCarrinho = DbFactory.Instance.ValorCarrinhoRepository.FindByIdString(sessionId);
            valorCarrinho.Total = valorCarrinho.Total + (carrinho.Produto.PrecoUnitario*quantidade);
            DbFactory.Instance.ValorCarrinhoRepository.SaveOrUpdate(valorCarrinho);

            return RedirectToAction("Index");
        }

        public ActionResult AplicarCupom(String cupom)
        {
            string sessionId = this.Session.SessionID;
            var valorCarrinho = DbFactory.Instance.ValorCarrinhoRepository.FindByIdString(sessionId);
            valorCarrinho.Cupom = cupom;
            DbFactory.Instance.ValorCarrinhoRepository.SaveOrUpdate(valorCarrinho);

            return RedirectToAction("Index");
        }

        public ActionResult AplicarFrete(String frete)
        {
            string sessionId = this.Session.SessionID;
            var valorCarrinho = DbFactory.Instance.ValorCarrinhoRepository.FindByIdString(sessionId);
            valorCarrinho.Total = valorCarrinho.Total -10;
            DbFactory.Instance.ValorCarrinhoRepository.SaveOrUpdate(valorCarrinho);

            return RedirectToAction("Index");
        }
    }
}