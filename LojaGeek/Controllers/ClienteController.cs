using LojaGeek.Model.DB;
using LojaGeek.Model.DB.Model;
using LojaGeek.Model.Utils;
using System.Web.Mvc;

namespace LojaGeek.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult CadastrarCliente()
        {
            return View(new Cliente());
        }

        public ActionResult CadastrarInteresses()
        {
            return View();
        }

        public ActionResult EntrarCliente()
        {
            return View();
        }

        public ActionResult Logar(string email, string senha)
        {
            LoginUtils.Logar(email, senha);

            if (LoginUtils.Cliente != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("EntrarCliente");
            }
        }

        public ActionResult DeslogarCliente()
        {
            LoginUtils.Deslogar();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GravarCliente(Cliente cliente, string acao, string rpg, string esporte, string aventura, string estrategia, string simulador)
        {
            if (acao != null)
                ColocarInteresseBD("Ação", cliente);
            if (rpg != null)
                ColocarInteresseBD("RPG", cliente);
            if (esporte != null)
                ColocarInteresseBD("Esporte", cliente);
            if (aventura != null)
                ColocarInteresseBD("Aventura", cliente);
            if (estrategia != null)
                ColocarInteresseBD("Estratégia", cliente);
            if (simulador != null)
                ColocarInteresseBD("Simulador", cliente);

            DbFactory.Instance.ClienteRepository.SaveOrUpdate(cliente);
            return RedirectToAction("Logar");
        }

        public void ColocarInteresseBD(string nome, Cliente cliente)
        {
            Interesse interesse = new Interesse();
            interesse.NomeInteresse = nome;
            interesse.Cliente = cliente;
            DbFactory.Instance.InteresseRepository.SaveOrUpdate(interesse);
        }
    }
}