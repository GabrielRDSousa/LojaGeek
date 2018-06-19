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
    }
}