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
            Carrinho carrinho = (Carrinho)Session["carrinho"];
            if (carrinho != null)
            {
                carrinho = DbFactory.Instance.CarrinhoRepository.FindById(carrinho.Id);
            }
            else
            {
                carrinho = new Carrinho();
            }
            Session["carrinho"] = carrinho;
            Double total = 0.0;
            foreach(var item in carrinho.Items)
            {
                total += (item.Produto.Preco * item.Quantidade);
            }
            if (carrinho.ValorFrete != null)
            {
                total += carrinho.ValorFrete;
            }
            if (carrinho.Cupom != null)
            {
                total -= ((total- carrinho.ValorFrete) * carrinho.Cupom.Desconto);
            }
            carrinho.ValorTotal = total;
            carrinho = DbFactory.Instance.CarrinhoRepository.SaveOrUpdate(carrinho);

            ViewBag.erro = (String)Session["erro"];
            ViewBag.sucesso = (String)Session["sucesso"];

            return View(carrinho);
        }

        public ActionResult ColocarCarrinho(Guid id, int quantidade)
        {
            var produto = DbFactory.Instance.ProdutoRepository.FindById(id);
            Carrinho carrinho = (Carrinho)Session["carrinho"];
            foreach(var ic in carrinho.Items)
            {
                if (ic.Produto.Id == produto.Id)
                {
                    if(ic.Quantidade < produto.Estoque)
                    {
                        ic.Quantidade += 1;
                        DbFactory.Instance.ItemCarrinhoRepository.SaveOrUpdate(ic);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }                                       
                }
            }


            ItemCarrinho item = new ItemCarrinho();
            item.Produto = produto;
            item.Quantidade = quantidade;
            item.Carrinho = carrinho;
            DbFactory.Instance.ItemCarrinhoRepository.SaveOrUpdate(item);
            return RedirectToAction("Index");

        }

        public ActionResult RetirarItem(Guid id)
        {
            var item = DbFactory.Instance.ItemCarrinhoRepository.FindById(id);
            DbFactory.Instance.ItemCarrinhoRepository.Delete(item);
            return RedirectToAction("Index");
        }

        public ActionResult AlterarQuantidade(Guid id, int quantidade_nova)
        {
            var item = DbFactory.Instance.ItemCarrinhoRepository.FindById(id);
            item.Quantidade = quantidade_nova;
            DbFactory.Instance.ItemCarrinhoRepository.SaveOrUpdate(item);
            return RedirectToAction("Index");
        }

        public ActionResult GravarCupom(String cupom)
        {
            var c = DbFactory.Instance.CupomRepository.GetByName(cupom);
            if (c != null)
            {
                Carrinho carrinho = (Carrinho)Session["carrinho"];
                if (carrinho != null)
                {
                    carrinho = DbFactory.Instance.CarrinhoRepository.FindById(carrinho.Id);
                    carrinho.Cupom = c;
                    DbFactory.Instance.CarrinhoRepository.SaveOrUpdate(carrinho);
                    carrinho = DbFactory.Instance.CarrinhoRepository.FindById(carrinho.Id);
                    Session["carrinho"] = carrinho;
                }
                else
                {
                    Session["erro"] = "Carrinho vazio, coloque algum produto no carrinho antes";
                }
            }
            else
            {
                Session["erro"] = "Cupom não existe";
            }
            return RedirectToAction("Index");
        }

        public ActionResult CalcularFrete(String frete)
        {
            Carrinho carrinho = (Carrinho)Session["carrinho"];
            if (carrinho != null)
            {
                carrinho = DbFactory.Instance.CarrinhoRepository.FindById(carrinho.Id);
                carrinho.ValorFrete = 10;
                carrinho = DbFactory.Instance.CarrinhoRepository.SaveOrUpdate(carrinho);
                Session["sucesso"] = "O frete é de 10 reais";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Comprar()
        {
            Carrinho carrinho = (Carrinho)Session["carrinho"];
            if (carrinho != null)
            {
                carrinho = DbFactory.Instance.CarrinhoRepository.FindById(carrinho.Id);
                var compra = new Compra();
                compra.Carrinho = carrinho;

                compra.Cliente = (Cliente)Session["usuario"];

                compra.EnderecoEntrega = null;

                DbFactory.Instance.CompraRespository.SaveOrUpdate(compra);

                foreach(ItemCarrinho item in compra.Carrinho.Items)
                {
                    var produto = item.Produto;
                    produto.Estoque -= item.Quantidade;
                    if(produto.Estoque == 0)
                    {
                        produto.Ativo = false;
                    }
                    DbFactory.Instance.ProdutoRepository.SaveOrUpdate(produto);
                }

                Session["carrinho"] = null;
                Session["sucesso"] = null;
                Session["erro"] = null;

                return View(compra);
            }
            else
            {
                return View("Index", "Home");
            }

            
        }
    }
}