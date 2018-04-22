using LojaGeek.Model.DB.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Model.DB.Repository
{
    public class ValorCarrinhoRepository : RepositoryBase<ValorCarrinho>
    {
        public ValorCarrinhoRepository(ISession session) : base(session) { }
        
        public ValorCarrinho FindByIdString(String id)
        {
            try
            {
                return Session.Get<ValorCarrinho>(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar pelo id", ex);
            }
        }
    }
}
