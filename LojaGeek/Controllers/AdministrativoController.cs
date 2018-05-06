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
    public class AdministrativoController : Controller
    {

        public ActionResult Estoque()
        {

            var produtos = DbFactory.Instance.ProdutoRepository.FindAll();          

            return View(produtos);
        }

        public ActionResult LoginView()
        {
            return View();
        }

        public ActionResult LoginAdministrativo(string senha)
        {
            if (LoginUtils.LoginAdministrativo(senha))
            {
                return RedirectToAction("Estoque");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DeslogarAdmin()
        {
            LoginUtils.DeslogarAdmin();
            return RedirectToAction("Index", "Home");
            
        }



        /*Manipulações com o produto*/
        public ActionResult DesativarProduto(Guid id)
        {
            var produto = DbFactory.Instance.ProdutoRepository.FindById(id);

            if (produto != null)
            {
                produto.Ativo = false;
                DbFactory.Instance.ProdutoRepository.SaveOrUpdate(produto);
            }

            return RedirectToAction("Estoque");
        }

        public ActionResult AtivarProduto(Guid id)
        {
            var produto = DbFactory.Instance.ProdutoRepository.FindById(id);

            if (produto != null)
            {
                produto.Ativo = true;
                DbFactory.Instance.ProdutoRepository.SaveOrUpdate(produto);
            }

            return RedirectToAction("Estoque");
        }

        //public ActionResult BuscarPeloNome(String edtBusca)
        //{
        //    if (String.IsNullOrEmpty(edtBusca))
        //    {
        //        return RedirectToAction("Estoque");
        //    }

        //    IList<Produto> produtos = new List<Produto>();
        //    produtos = DbFactory.Instance.ProdutoRepository.GetAllByName(edtBusca);

        //    return RedirectToAction("Estoque", produtos);
        //}

        public ActionResult AdicionarEstoque(Guid id)
        {
            var produto = DbFactory.Instance.ProdutoRepository.FindById(id);
            return View(produto);
        }

        public ActionResult ModificarEstoque(Produto produto)
        {
            var produtoDestualizado = DbFactory.Instance.ProdutoRepository.FindById(produto.Id);
            var precoAtualizado = CalculoUtils.NovoPrecoAoAtualizarEstoque(produto, produtoDestualizado);
            var quantidadeAdicionada = produto.Estoque;

            produto = produtoDestualizado;

            produto.Preco = precoAtualizado;
            produto.Estoque += quantidadeAdicionada;

            DbFactory.Instance.ProdutoRepository.SaveOrUpdate(produto);

            return RedirectToAction("Estoque");
        }

    }
}