using LojaGeek.Model.DB;
using LojaGeek.Model.DB.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LojaGeek.Model.Utils
{
    public class LoginUtils
    {
        public static Cliente Cliente
        {
            get
            {
                if (HttpContext.Current.Session["Usuario"] != null)
                {
                    return (Cliente)HttpContext.Current.Session["Usuario"];
                }
                else
                    return null;
            }
        }

        public static String Admin
        {
            get
            {
                if (HttpContext.Current.Session["Admin"] != null)
                {
                    return (String)HttpContext.Current.Session["Admin"];
                }
                else
                    return null;
            }
        }

        public static void Logar(string email, string senha)
        {
            var cliente = DbFactory.Instance.ClienteRepository.Login(email, senha);
            if (cliente != null)
            {
                HttpContext.Current.Session["Usuario"] = cliente;
            }
        }

        public static void Deslogar()
        {
            HttpContext.Current.Session["Usuario"] = null;
            HttpContext.Current.Session.Remove("Usuario");
        }

        public static bool LoginAdministrativo(string senha)
        {
            var culture = new CultureInfo("pt-BR");
            var v1 = culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek).ToString().Substring(0, 3);
            var v2 = DateTime.Now.Month.ToString();
            var dia = DateTime.Now.Day;
            var hora = DateTime.Now.Hour;
            var v3 = (dia + hora).ToString();

            string senhaCalculada = v1 + v2 + v3;

            if (senhaCalculada == senha)
            {
                HttpContext.Current.Session["Admin"] = "admin";
                return true;
            }
            else
            {
                HttpContext.Current.Session["Admin"] = null;
                return false;
            }

        }

        public static void DeslogarAdmin()
        {
            HttpContext.Current.Session["Admin"] = null;
            HttpContext.Current.Session.Remove("Admin");
        }
    }
}
