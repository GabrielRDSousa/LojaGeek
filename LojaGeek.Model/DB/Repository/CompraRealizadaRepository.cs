using LojaGeek.Model.DB.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Model.DB.Repository
{
    public class CompraRealizadaRepository : RepositoryBase<CompraRealizada>
    {
        public CompraRealizadaRepository(ISession session) : base(session)
        {
        }

        public IList<CompraRealizada> GetAllBySession(String Sessao)
        {
            try
            {
                return this.Session.Query<CompraRealizada>()
                           .Where(w => w.Sessao == Sessao).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar compra", ex);
            }
        }
    }
}
