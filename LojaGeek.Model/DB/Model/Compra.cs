using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Model.DB.Model
{
    public class Compra
    {
        public virtual Guid Id { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Endereco EnderecoEntrega { get; set; }
        public virtual DateTime DataDaCompra { get; set; }
        public virtual String MetodoDePagamento { get; set; }
        public virtual int QtdParcelas { get; set; }
        public virtual Carrinho Carrinho { get; set; }
        public virtual String Nfe { get; set; }

        public Compra()
        {
            DataDaCompra = DateTime.Now;
        }

        public class CompraMap : ClassMapping<Compra>
        {
            public CompraMap()
            {
                Id(x => x.Id, m => m.Generator(Generators.Guid));
                Property(x => x.DataDaCompra);
                Property(x => x.MetodoDePagamento);
                Property(x => x.QtdParcelas);
                Property(x => x.Nfe);

                ManyToOne(x => x.Cliente, m =>
                {
                    m.Column("ClienteId");
                    m.Lazy(LazyRelation.NoLazy);
                    m.Cascade(Cascade.Refresh);
                });

                ManyToOne(x => x.EnderecoEntrega, m =>
                {
                    m.Column("EnderecoId");
                    m.Lazy(LazyRelation.NoLazy);
                    m.Cascade(Cascade.Refresh);
                });

                ManyToOne(x => x.Carrinho, m =>
                {
                    m.Column("CarrinhoId");
                    m.Lazy(LazyRelation.NoLazy);
                    m.Cascade(Cascade.Refresh);
                });
            }
        }
    }
}
