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
        public virtual Cliente Cliente { get; set; }
        public virtual String Sessao { get; set; }
        public virtual Double ValorTotal { get; set; }
        public virtual Cupom Cupom { get; set; }
        public virtual Double ValorFrete { get; set; }
        public virtual IList<ItemCarrinho> Items { get; set; }

        public Carrinho()
        {
            Items = new List<ItemCarrinho>();
        }
    }

    public class CarrinhoMap : ClassMapping<Carrinho>
    {
        public CarrinhoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));
            Property(x => x.Sessao);
            Property(x => x.ValorFrete);
            Property(x => x.ValorTotal);

            ManyToOne(x => x.Cliente, m => {
                m.Column("ClienteId");
                m.Lazy(LazyRelation.NoLazy);
                m.Cascade(Cascade.Refresh);
            });

            ManyToOne(x => x.Cupom, m => {
                m.Column("CupomId");
                m.Lazy(LazyRelation.NoLazy);
                m.Cascade(Cascade.Refresh);
            });

            Bag(x => x.Items, m =>
            {
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
                m.Key(k => k.Column("CarrinhoId"));
            }, r => r.OneToMany());
        }
    }
}
