using LojaGeek.Model.DB.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Model.DB.Repository
{
    public class CompraRespository : RepositoryBase<Compra>
    {
        public CompraRespository(ISession session) : base(session)
        {
        }

        public IList<Compra> GetAllByClient(Cliente cliente)
        {
            try
            {
                return this.Session.Query<Compra>()
                           .Where(w => w.Cliente.Id.Equals(cliente.Id)).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
