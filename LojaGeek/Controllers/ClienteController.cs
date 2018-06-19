using LojaGeek.Model.DB;
using LojaGeek.Model.DB.Model;
using LojaGeek.Model.Utils;
using System;
using System.Security.Cryptography;
using System.Text;
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

        public ActionResult EntrarCliente(string retorno)
        {
            ViewBag.retorno = retorno;
            return View();
        }

        public ActionResult Logar(string email, string senha, string retorno)
        {
            var aux = GerarHashMd5(senha);
            senha = aux;
            LoginUtils.Logar(email, senha);

            if (LoginUtils.Cliente != null)
            {
                if (retorno.Equals("index"))
                {
                    Cliente cliente = (Cliente)Session["Usuario"];
                    TempData["success"] = "Bem vindo "+cliente.Nome;
                    return RedirectToAction("Index", "Home");
                }
                else if(retorno.Equals("compra"))
                {
                    TempData["success"] = "Continuando a compra :D";
                    return RedirectToAction("Index", "Endereco", new { escolha = "true" });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                
            }
            else
            {
                TempData["error"] = "Login ou senha incorretos(s)";
                return RedirectToAction("EntrarCliente");
            }
        }

        public ActionResult DeslogarCliente()
        {
            LoginUtils.Deslogar();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GravarCliente(Cliente cliente, string acao, string rpg, string esporte, string aventura, string estrategia, string simulador, string ps4, string ps3, string xOne, string x360, string nSwitch, string n3ds)
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
            if (ps4 != null)
                ColocarInteresseBD("Playstation 4", cliente);
            if (ps3 != null)
                ColocarInteresseBD("Playstation 3", cliente);
            if (xOne != null)
                ColocarInteresseBD("Xbox One", cliente);
            if (x360 != null)
                ColocarInteresseBD("Xbox 360", cliente);
            if (nSwitch != null)
                ColocarInteresseBD("Switch", cliente);
            if (n3ds != null)
                ColocarInteresseBD("Nintendo 3DS", cliente);

            try
            {
                var aux = GerarHashMd5(cliente.Senha);
                cliente.Senha = aux;
                DbFactory.Instance.ClienteRepository.SaveOrUpdate(cliente);
                TempData["success"] = "Cadastrado, pode fazer o login :D";
                return RedirectToAction("EntrarCliente");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("EntrarCliente");
            }

            
        }

        public void ColocarInteresseBD(string nome, Cliente cliente)
        {
            Interesse interesse = new Interesse();
            interesse.NomeInteresse = nome;
            interesse.Cliente = cliente;
            try
            {
                DbFactory.Instance.InteresseRepository.SaveOrUpdate(interesse);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            
        }

        public static string GerarHashMd5(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Converter a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Cria-se um StringBuilder para recompôr a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}