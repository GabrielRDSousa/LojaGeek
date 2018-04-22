using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Model.DB.Model
{
    public class Carrinho
    {
        public virtual Guid Id { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual int Quantidade { get; set; }
        public virtual String Sessao { get; set; }
    }

    public class CarrinhoMap : ClassMapping<Carrinho>
    {
        public CarrinhoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));
            Property(x => x.Sessao);
            Property(x => x.Quantidade);

            ManyToOne(x => x.Produto, m => {
                m.Column("ProdutoId");
                m.Lazy(LazyRelation.NoLazy);
                m.Cascade(Cascade.Persist);
            });

            ManyToOne(x => x.Produto, m => {
                m.Column("ClienteId");
                m.Lazy(LazyRelation.NoLazy);
                m.Cascade(Cascade.Persist);
            });
        }
    }
}
