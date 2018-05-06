using LojaGeek.Model.DB.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Model.DB.Repository
{
    public class ItemCarrinhoRepository : RepositoryBase<ItemCarrinho>
    {
        public ItemCarrinhoRepository(ISession session) : base(session){}

        public IList<ItemCarrinho> GetAllByCarrinho(Carrinho Carrinho)
        {
            try
            {
                return this.Session.Query<ItemCarrinho>()
                           .Where(w => w.Carrinho.Id == Carrinho.Id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar carrinho", ex);
            }
        }
    }
}
