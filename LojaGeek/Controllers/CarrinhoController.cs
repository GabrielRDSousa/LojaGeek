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
            var usuario = Session["usuario"];
            if(usuario == null)
            {
                return RedirectToAction("EntrarCliente","Cliente",new { retorno = "compra"});
            }
            else
            {
                return RedirectToAction("Index", "Endereco", new { escolha = "true" });
            }
        }

        public ActionResult MetodoPagamento(Guid endereco_escolhido)
        {
            var endereco = DbFactory.Instance.EnderecoRepository.FindById(endereco_escolhido);
            Session["endereco"] = endereco;
            return View();
        }

        public ActionResult Cartao(String cod_seguranca, String num_cartao, String parcelas, String vencimento, String nome_cartao)
        {
            
            Cliente cliente = (Cliente)Session["usuario"];
            Carrinho carrinho = (Carrinho)Session["carrinho"];
            Endereco endereco = (Endereco)Session["endereco"];
            Guid id_endereco = endereco.Id;

            validarCartao.Card server = new validarCartao.Card();
            validarCartao.tDadosCartao cartao = new validarCartao.tDadosCartao();
            cartao.CNPJEmpresa = 999999999;
            cartao.NomeEmpresa = cliente.Nome;
            cartao.NomeCliente = nome_cartao;
            cartao.Codigo = int.Parse(cod_seguranca);
            cartao.NumeroCartao = num_cartao;
            cartao.Parcelas = int.Parse(parcelas);
            cartao.Validade = vencimento;
            cartao.Valor = carrinho.ValorTotal;

            try
            {
                server.ValidarCartao(cartao);
            }
            catch(Exception ex)
            {
                Session["resposta_cartao"] = ex.Message;
                return RedirectToAction("MetodoPagamento", new { endereco_escolhido = id_endereco});
            }

            Session["resposta_cartao"] = "Cartão cadastrado com sucesso";
            Session["cartao"] = cartao;
            return RedirectToAction("MetodoPagamento", new { endereco_escolhido = id_endereco });
        }

        public ActionResult Resumo(String metodo)
        {
            Carrinho carrinho = (Carrinho)Session["carrinho"];
            ViewBag.metodo = metodo;
            if (metodo.Equals("boleto"))
            {
                var aux = carrinho.ValorTotal - (carrinho.ValorTotal * 0.1);
                carrinho.ValorTotal = aux;
                return View();
            }
            else
            {
                return View();
            }
        }

        public ActionResult Confirmar(String metodo)
        {
            Compra compra = new Compra();
            validarCartao.tDadosCartao cartao = (validarCartao.tDadosCartao)Session["cartao"];
            compra.Carrinho = (Carrinho)Session["carrinho"];
            compra.Cliente = (Cliente)Session["usuario"];
            compra.DataDaCompra = DateTime.Now;
            compra.EnderecoEntrega = (Endereco)Session["endereco"];
            compra.MetodoDePagamento = metodo;
            if (metodo.Equals("boleto"))
            {
                compra.QtdParcelas = 1;
            }
            else
            {
                compra.QtdParcelas = cartao.Parcelas;
            }

            DbFactory.Instance.CompraRespository.SaveOrUpdate(compra);

            foreach (ItemCarrinho item in compra.Carrinho.Items)
            {
                var produto = item.Produto;
                produto.Estoque -= item.Quantidade;
                if (produto.Estoque == 0)
                {
                    produto.Ativo = false;
                }
                DbFactory.Instance.ProdutoRepository.SaveOrUpdate(produto);
            }

            Session["carrinho"] = null;
            Session["endereco"] = null;
            Session["cartao"] = null;

            return View();
        }


        //public ActionResult Comprar()
        //{
        //    Carrinho carrinho = (Carrinho)Session["carrinho"];
        //    if (carrinho != null)
        //    {
        //        carrinho = DbFactory.Instance.CarrinhoRepository.FindById(carrinho.Id);
        //        var compra = new Compra();
        //        compra.Carrinho = carrinho;

        //        compra.Cliente = (Cliente)Session["usuario"];

        //        compra.EnderecoEntrega = null;

        //        DbFactory.Instance.CompraRespository.SaveOrUpdate(compra);

        //        foreach(ItemCarrinho item in compra.Carrinho.Items)
        //        {
        //            var produto = item.Produto;
        //            produto.Estoque -= item.Quantidade;
        //            if(produto.Estoque == 0)
        //            {
        //                produto.Ativo = false;
        //            }
        //            DbFactory.Instance.ProdutoRepository.SaveOrUpdate(produto);
        //        }

        //        Session["carrinho"] = null;
        //        Session["sucesso"] = null;
        //        Session["erro"] = null;

        //        return View(compra);
        //    }
        //    else
        //    {
        //        return View("Index", "Home");
        //    }


        //}
    }
}