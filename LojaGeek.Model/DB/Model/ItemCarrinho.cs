using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Model.DB.Model
{
    public class ItemCarrinho
    {
        public virtual Guid Id { get; set; }
        public virtual Carrinho Carrinho { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual int Quantidade { get; set; }

        public class ItemCarrinhoMap : ClassMapping<ItemCarrinho>
        {
            public ItemCarrinhoMap()
            {
                Id(x => x.Id, m => m.Generator(Generators.Guid));
                Property(x => x.Quantidade);

                ManyToOne(x => x.Carrinho, m => {
                    m.Column("CarrinhoId");
                    m.Lazy(LazyRelation.NoLazy);
                    m.Cascade(cascadeStyle: Cascade.Refresh);
                });

                ManyToOne(x => x.Produto, m => {
                    m.Column("ProdutoId");
                    m.Lazy(LazyRelation.NoLazy);
                    m.Cascade(cascadeStyle: Cascade.Refresh);
                });
            }
        }
    }
}
