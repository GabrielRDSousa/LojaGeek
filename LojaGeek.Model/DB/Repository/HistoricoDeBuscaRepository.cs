using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaGeek.Model.DB.Model;
using NHibernate;

namespace LojaGeek.Model.DB.Repository
{
    public class HistoricoDeBuscaRepository : RepositoryBase<HistoricoDeBusca>
    {
        public HistoricoDeBuscaRepository(ISession session) : base(session){}

        public IList<HistoricoDeBusca> FindByCliente(Cliente cliente)
        {
            try
            {
                return this.Session.Query<HistoricoDeBusca>()
                           .Where(w => w.Cliente == cliente).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar a lista", ex);
            }
        }
    }
}
