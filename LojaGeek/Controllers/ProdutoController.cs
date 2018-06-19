using LojaGeek.Model.DB;
using LojaGeek.Model.DB.Model;
using LojaGeek.Model.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//TempData["warning"] = "Mensagem de warning!!";
//TempData["success"] = "Mensagem de sucesso!!";
//TempData["info"] = "Mensagem de informação!!";
//TempData["error"] = "Mensagem de erro!!";

namespace LojaGeek.Controllers
{
    public class ProdutoController : Controller
    {

        public Boolean EhAdmin()
        {
            var admin = Session["Admin"];
            if (admin != null)
                return true;
            else
                return false;
        }

        public ActionResult CadastrarProduto()
        {
            if (EhAdmin())
            {
                return View();
            }
            else
            {
                TempData["warning"] = "Área restrita, necessário autenticação";
                return RedirectToAction("LoginView", "Administrativo");
            }
        }

        public ActionResult GravarProduto(Produto produto, HttpPostedFileBase file)
        {
            if (EhAdmin())
            {
                var precoFabrica = produto.Preco;
                var precoFinal = CalculoUtils.CalcularPrecoReal(precoFabrica);
                produto.Preco = precoFinal;

                try
                {
                    var p = DbFactory.Instance.ProdutoRepository.SaveOrUpdate(produto);
                    try
                    {
                        if (file.ContentLength > 0)
                        {
                            string _FileName = p.Id.ToString() + ".jpg";
                            string _path = Path.Combine(Server.MapPath("~/Images"), _FileName);
                            produto.Foto = "/Images/"+ p.Id.ToString() + ".jpg";
                            file.SaveAs(_path);
                            DbFactory.Instance.ProdutoRepository.SaveOrUpdate(p);
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["error"] = ex.Message;
                    }

                    TempData["success"] = "Produto adicionado com sucesso";
                }catch(Exception ex)
                {
                    TempData["error"] = ex.Message;
                }
                
                return RedirectToAction("Estoque", "Administrativo");
            }
            else
            {
                TempData["warning"] = "Área restrita, necessário autenticação";
                return RedirectToAction("LoginView", "Administrativo");
            }
        }


        public ActionResult Detalhes(Guid id)
        {
            Produto produto;
            try
            {
                produto = DbFactory.Instance.ProdutoRepository.FindById(id);
                return View(produto);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
            
        }
    }
}