using LojaGeek.Model.DB;
using LojaGeek.Model.DB.Model;
using LojaGeek.Model.Utils;
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
    public class AdministrativoController : Controller
    {
        public Boolean EhAdmin()
        {
            var admin = Session["Admin"];
            if (admin != null)
                return true;
            else
                return false;
        }

        public ActionResult Estoque()
        {
            if (EhAdmin())
            {
                try
                {
                    var produtos = DbFactory.Instance.ProdutoRepository.FindAll();
                    return View(produtos);
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["warning"] = "Área restrita, necessário autenticação";
                return RedirectToAction("LoginView");
            }
            
        }

        public ActionResult Cupons()
        {
            if (EhAdmin())
            {
                try
                {
                    var cupons = DbFactory.Instance.CupomRepository.FindAll();
                    return View(cupons);
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["warning"] = "Área restrita, necessário autenticação";
                return RedirectToAction("LoginView");
            }

        }

        public ActionResult CriarCupom()
        {
            if (EhAdmin())
            {
                return View();
            }
            else
            {
                TempData["warning"] = "Área restrita, necessário autenticação";
                return RedirectToAction("LoginView");
            }

        }

        public ActionResult GravarCupom(Cupom cupom)
        {
            if (EhAdmin())
            {
                TempData["success"] = "Cupom criado com sucesso";
                DbFactory.Instance.CupomRepository.SaveOrUpdate(cupom);
                return RedirectToAction("Cupons");
            }
            else
            {
                TempData["warning"] = "Área restrita, necessário autenticação";
                return RedirectToAction("LoginView");
            }

        }

        public ActionResult LoginView()
        {
            return View();
        }

        public ActionResult LoginAdministrativo(string senha)
        {
            if (LoginUtils.LoginAdministrativo(senha))
            {
                
                TempData["success"] = "Bem vindo administrador";
                return RedirectToAction("Estoque");
            }
            else
            {
                TempData["error"] = "Senha incorreta, tente novamente";
                return RedirectToAction("LoginView");
            }
        }

        public ActionResult DeslogarAdmin()
        {
            LoginUtils.DeslogarAdmin();
            TempData["success"] = "Até mais administrador";
            return RedirectToAction("Index", "Home");
            
        }

        public ActionResult DesativarProduto(Guid id)
        {
            if (EhAdmin())
            {
                try
                {
                    var produto = DbFactory.Instance.ProdutoRepository.FindById(id);
                    if (produto.Ativo != false)
                    {
                        TempData["success"] = "Produto desativado";
                        produto.Ativo = false;
                        DbFactory.Instance.ProdutoRepository.SaveOrUpdate(produto);
                    }
                    else
                    {
                        TempData["warning"] = "Produto já está inativo";
                    }

                    return RedirectToAction("Estoque");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return RedirectToAction("Estoque");
                }
            }
            else
            {
                TempData["warning"] = "Área restrita, necessário autenticação";
                return RedirectToAction("LoginView");
            }
            
        }

        public ActionResult AtivarProduto(Guid id)
        {
            if (EhAdmin())
            {
                try
                {
                    var produto = DbFactory.Instance.ProdutoRepository.FindById(id);

                    if (produto.Ativo != true)
                    {
                        TempData["success"] = "Produto ativado";
                        produto.Ativo = true;
                        DbFactory.Instance.ProdutoRepository.SaveOrUpdate(produto);
                    }
                    else
                    {
                        TempData["warning"] = "Produto já está ativo";
                    }

                    return RedirectToAction("Estoque");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return RedirectToAction("Estoque");
                }

            }
            else
            {
                TempData["warning"] = "Área restrita, necessário autenticação";
                return RedirectToAction("LoginView");
            }

            
        }

        public ActionResult AtivarCupom(Guid id)
        {
            if (EhAdmin())
            {
                try
                {
                    var cupom = DbFactory.Instance.CupomRepository.FindById(id);
                    if (cupom.FoiUsado != false)
                    {
                        TempData["success"] = "Cupom ativado";
                        cupom.FoiUsado = false;
                        DbFactory.Instance.CupomRepository.SaveOrUpdate(cupom);
                    }
                    else
                    {
                        TempData["warning"] = "Cupom já está ativo";
                    }

                    return RedirectToAction("Cupons");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return RedirectToAction("Cupons");
                }
            }
            else
            {
                TempData["warning"] = "Área restrita, necessário autenticação";
                return RedirectToAction("LoginView");
            }

        }

        public ActionResult DesativarCupom(Guid id)
        {
            if (EhAdmin())
            {
                try
                {
                    var cupom = DbFactory.Instance.CupomRepository.FindById(id);

                    if (cupom.FoiUsado != true)
                    {
                        TempData["success"] = "Cupom desativado";
                        cupom.FoiUsado = true;
                        DbFactory.Instance.CupomRepository.SaveOrUpdate(cupom);
                    }
                    else
                    {
                        TempData["warning"] = "Cupom já está inativo";
                    }

                    return RedirectToAction("Cupons");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return RedirectToAction("Cupons");
                }

            }
            else
            {
                TempData["warning"] = "Área restrita, necessário autenticação";
                return RedirectToAction("LoginView");
            }


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
            if (EhAdmin())
            {
                try
                {
                    var produto = DbFactory.Instance.ProdutoRepository.FindById(id);
                    return View(produto);
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex;
                    return RedirectToAction("Estoque");
                }
            }
            else
            {
                TempData["warning"] = "Área restrita, necessário autenticação";
                return RedirectToAction("LoginView");
            }

        }

        public ActionResult ModificarEstoque(Produto produto)
        {
            if (EhAdmin())
            {
                try
                {
                    var produtoDestualizado = DbFactory.Instance.ProdutoRepository.FindById(produto.Id);
                    var precoAtualizado = CalculoUtils.NovoPrecoAoAtualizarEstoque(produto, produtoDestualizado);
                    var quantidadeAdicionada = produto.Estoque;

                    produto = produtoDestualizado;

                    produto.Preco = precoAtualizado;
                    produto.Estoque += quantidadeAdicionada;

                    DbFactory.Instance.ProdutoRepository.SaveOrUpdate(produto);

                    TempData["success"] = "Produto modificado com sucesso";
                    return RedirectToAction("Estoque");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex;
                    return RedirectToAction("Estoque");
                }
                
            }
            else
            {
                TempData["warning"] = "Área restrita, necessário autenticação";
                return RedirectToAction("LoginView");
            }

        }

    }
}