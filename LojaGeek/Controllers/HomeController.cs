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
        public ActionResult Index(String busca)
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
            if(busca == null)
            {
                var produtosAux = DbFactory.Instance.ProdutoRepository.FindAll();
                var produtos = new List<Produto>();
                foreach (Produto produto in produtosAux)
                {
                    if (produto.Ativo && produto.Estoque > 0)
                    {
                        produtos.Add(produto);
                    }
                }
                return View(produtos);
            }
            else
            {
                if(busca != "Playstation 4" || busca != "Playstation 3" || busca != "Xbox One" || busca != "Xbox 360" || busca != "Switch" || busca != "Nintendo 3DS")
                {
                    var produtosAux = DbFactory.Instance.ProdutoRepository.GetAllByName(busca);
                    var produtos = new List<Produto>();
                    foreach (Produto produto in produtosAux)
                    {
                        if (produto.Ativo && produto.Estoque > 0)
                        {
                            produtos.Add(produto);
                        }
                    }
                    var cliente = (Cliente)Session["Usuario"];
                    if (cliente != null)
                    {
                        HistoricoDeBusca histBusca = new HistoricoDeBusca();
                        histBusca.Cliente = cliente;
                        histBusca.Pesquisa = busca;
                        try
                        {
                            DbFactory.Instance.HistoricoDeBuscaRepository.SaveOrUpdate(histBusca);
                        }
                        catch (Exception ex)
                        {
                            TempData["error"] = "Erro ao gravar essa pesquisa, "+ex.Message;
                        }
                    }
                    return View(produtos);
                }
                else
                {
                    var produtosAux = DbFactory.Instance.ProdutoRepository.GetAllByPlataforma(busca);
                    var produtos = new List<Produto>();
                    foreach (Produto produto in produtosAux)
                    {
                        if (produto.Ativo && produto.Estoque > 0)
                        {
                            produtos.Add(produto);
                        }
                    }

                    return View(produtos);
                }
                
            }
            
        }

        public ActionResult DetalheProduto(Guid id)
        {
            return RedirectToAction("Detalhes","Produto", new { id=id });
        }

        public ActionResult HistoricoDeBusca()
        {
            var cliente = (Cliente)Session["Usuario"];
            var lstBuscas = DbFactory.Instance.HistoricoDeBuscaRepository.FindByCliente(cliente);
            return View(lstBuscas);
        }
    }
}