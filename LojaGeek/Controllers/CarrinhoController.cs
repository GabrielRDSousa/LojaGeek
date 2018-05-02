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
            foreach (var item in carrinho) { total = total + (item.Produto.PrecoUnitario * item.Quantidade); }

            var cupom = this.Session["cupom"];
            if(cupom != null) { total = total - (total * 0.1); }
            ViewBag.Cupom = cupom;

            ViewBag.Total = total;

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
            Carrinho carrinho = DbFactory.Instance.CarrinhoRepository.FindById(id);
            DbFactory.Instance.CarrinhoRepository.Delete(carrinho);
            return RedirectToAction("Index");
        }

        public ActionResult Comprar()
        {
            string sessionId = this.Session.SessionID;
            var carrinho = DbFactory.Instance.CarrinhoRepository.GetAllBySession(sessionId);
            var compraRealizada = new CompraRealizada();
            Cliente cliente = (Cliente) this.Session["Usuario"];
            foreach(var item in carrinho)
            {
                compraRealizada.Produto = item.Produto;
                compraRealizada.Cliente = cliente;
                compraRealizada.DataCompra = DateTime.Now;
                compraRealizada.Quantidade = item.Quantidade;
                compraRealizada.Sessao = item.Sessao;

                var produto = DbFactory.Instance.ProdutoRepository.FindById(item.Produto.Id);
                produto.QuantidadeEstoque = produto.QuantidadeEstoque - item.Quantidade;
                DbFactory.Instance.ProdutoRepository.SaveOrUpdate(produto);
                DbFactory.Instance.CarrinhoRepository.Delete(item);
            }
            var compra = DbFactory.Instance.CompraRealizadaRepository.GetAllBySession(sessionId);
            ViewBag.Compra = compra;
            return View();
        }

        public ActionResult AtualizarTotal(Guid id_item_carrinho, int quantidade)
        {
            var carrinho = DbFactory.Instance.CarrinhoRepository.FindById(id_item_carrinho);
            if (quantidade > 0)
            {
                carrinho.Quantidade = quantidade;
                DbFactory.Instance.CarrinhoRepository.SaveOrUpdate(carrinho);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("RetirarDoCarrinho", new { id=id_item_carrinho});
            }
        }

        public ActionResult AplicarCupom(String cupom)
        {
            this.Session["cupom"] = cupom;
            ViewBag.Cupom = cupom;
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