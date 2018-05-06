using LojaGeek.Model.DB.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Model.DB.Repository
{
    public class InteresseRepository : RepositoryBase<Interesse>
    {
        public InteresseRepository(ISession session) : base(session) { }

        public IList<Interesse> GetAllByCliente(Cliente cliente)
        {
            try
            {
                return this.Session.Query<Interesse>()
                           .Where(w => w.Cliente.Id == cliente.Id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar pessoa pelo nome ", ex);
            }
        }
    }
}
