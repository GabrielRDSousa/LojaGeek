using LojaGeek.Model.DB;
using LojaGeek.Model.DB.Model;
using LojaGeek.Model.DB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//TempData["warning"] = "Mensagem de warning!!";
//TempData["success"] = "Mensagem de sucesso!!";
//TempData["info"] = "Mensagem de informação!!";
//TempData["error"] = "Mensagem de erro!!";

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

            carrinho.ValorTotal = total;

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
                if (carrinho != null || carrinho.Items.Count > 0)
                {
                    if (!c.FoiUsado)
                    {
                        carrinho.Cupom = c;
                        DbFactory.Instance.CarrinhoRepository.SaveOrUpdate(carrinho);
                        Session["carrinho"] = carrinho;
                        TempData["success"] = "Cupom aplicado";
                    }
                    else
                    {
                        TempData["error"] = "Cupom já utilizado";
                    }
                    
                }
                else
                {
                    TempData["error"] = "Carrinho vazio, coloque algum produto no carrinho antes";
                }
            }
            else
            {
                TempData["error"] = "Cupom não existe";
            }
            return RedirectToAction("Index");
        }

        public ActionResult CalcularFrete(String frete)
        {
            Carrinho carrinho = (Carrinho)Session["carrinho"];
            Session["Cep"] = frete;
            if (carrinho != null)
            {
                carrinho.ValorFrete = 10.00;
                carrinho = DbFactory.Instance.CarrinhoRepository.SaveOrUpdate(carrinho);
                TempData["success"] = "Frete será 10 reais";
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

            num_cartao = num_cartao.Replace(" ", "");
            var vencimentoDividido = vencimento.Split('/');
            vencimento = vencimentoDividido[1] + vencimentoDividido[0];


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
                TempData["error"] = ex.Message;
                return RedirectToAction("MetodoPagamento", new { endereco_escolhido = id_endereco});
            }

            TempData["success"] = "Cartão autorizado";
            Session["cartao"] = cartao;
            return RedirectToAction("Resumo", new { metodo = "cartao" });
        }

        public ActionResult Resumo(String metodo)
        {
            Carrinho carrinho = (Carrinho)Session["carrinho"];
            ViewBag.metodo = metodo;
            if (metodo.Equals("boleto"))
            {
                if(carrinho.Cupom == null)
                {
                    Cupom cupom = new Cupom();
                    cupom.Desconto = 10;
                    cupom.Nome = "Boleto";
                    try
                    {
                        DbFactory.Instance.CupomRepository.SaveOrUpdate(cupom);
                        carrinho.Cupom = cupom;
                    }
                    catch(Exception ex)
                    {
                        TempData["error"] = ex.Message;
                    }
                }
                if (carrinho.ValorFrete == 0)
                {
                    carrinho.ValorFrete = 10.00;
                    DbFactory.Instance.CarrinhoRepository.SaveOrUpdate(carrinho);
                }
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

            if (compra.Carrinho.Cupom != null)
            {
                compra.Carrinho.Cupom.FoiUsado = true;
                DbFactory.Instance.CupomRepository.SaveOrUpdate(compra.Carrinho.Cupom);
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