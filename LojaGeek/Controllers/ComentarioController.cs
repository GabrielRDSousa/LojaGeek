using LojaGeek.Model.DB;
using LojaGeek.Model.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaGeek.Controllers
{
    public class ComentarioController : Controller
    {
        public Boolean EhAdmin()
        {
            var admin = Session["Admin"];
            if (admin != null)
                return true;
            else
                return false;
        }

        // GET: Comentario
        public ActionResult GravarComentario(string comentario, string nome, int nota, Guid idProduto)
        {
            Comentario coment = new Comentario();
            Produto produto = DbFactory.Instance.ProdutoRepository.FindById(idProduto);
            coment.Nome = nome;
            coment.Texto = comentario;
            coment.Produto = produto;
            coment.Nota = nota;
            DbFactory.Instance.ComentarioRepository.SaveOrUpdate(coment);
            return RedirectToAction("Detalhes", "Produto", new { id = produto.Id});
        }

        public ActionResult DesativarComentario(Guid id)
        {
            Comentario coment = DbFactory.Instance.ComentarioRepository.FindById(id);
            if (EhAdmin())
            {
                try
                {
                    coment.Visivel = false;
                    DbFactory.Instance.ComentarioRepository.SaveOrUpdate(coment);
                    TempData["success"] = "Comentário desativado";
                }
                catch(Exception ex)
                {
                    TempData["error"] = ex.Message;
                }
                
            }
            return RedirectToAction("Detalhes", "Produto", new { id = coment.Produto.Id });
        }

        public ActionResult AtivarComentario(Guid id)
        {
            Comentario coment = DbFactory.Instance.ComentarioRepository.FindById(id); ;
            if (EhAdmin())
            {
                try
                {
                    coment.Visivel = true;
                    DbFactory.Instance.ComentarioRepository.SaveOrUpdate(coment);
                    TempData["success"] = "Comentário ativado";
                }
                catch(Exception ex)
                {
                    TempData["error"] = ex.Message;
                }
            }
            
            return RedirectToAction("Detalhes", "Produto", new { id = coment.Produto.Id });
        }


    }
}