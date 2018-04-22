using LojaGeek.Model.DB.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LojaGeek.Model.DB.Repository
{
    public class CarrinhoRepository : RepositoryBase<Carrinho>
    {
        public CarrinhoRepository(ISession session) : base(session) { }

        public IList<Carrinho> GetAllBySession(String Sessao)
        {
            try
            {
                return this.Session.Query<Carrinho>()
                           .Where(w => w.Sessao == Sessao).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar carrinho", ex);
            }
        }
    }
}
