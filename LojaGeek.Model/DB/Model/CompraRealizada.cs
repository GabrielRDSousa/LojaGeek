using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Model.DB.Model
{
    public class CompraRealizada
    {
        public virtual Guid Id { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual int Quantidade { get; set; }
        public virtual String Sessao { get; set; }
        public virtual DateTime DataCompra { get; set; }
    }

    public class CompraMap : ClassMapping<CompraRealizada>
    {
        public CompraMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));
            Property(x => x.Quantidade);
            Property(x => x.DataCompra);
            Property(x => x.Sessao);

            ManyToOne(x => x.Produto, m => {
                m.Column("ProdutoId");
                m.Lazy(LazyRelation.NoLazy);
                m.Cascade(Cascade.Persist);
            });

            ManyToOne(x => x.Cliente, m => {
                m.Column("ClienteId");
                m.Lazy(LazyRelation.NoLazy);
                m.Cascade(Cascade.Persist);
            });
        }
    }
}
