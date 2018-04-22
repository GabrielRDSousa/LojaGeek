using LojaGeek.Model.DB.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Model.DB.Repository
{
    public class ProdutoRepository : RepositoryBase<Produto>
    {
        public ProdutoRepository(ISession session) : base(session) { }

        public IList<Produto> GetAllByName(String nome)
        {
            try
            {
                return this.Session.Query<Produto>()
                           .Where(w => w.Nome.ToLower() == nome.Trim().ToLower()).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar pessoa pelo nome", ex);
            }
        }
    }
}
