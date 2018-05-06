using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Model.DB.Model
{
    public class Cupom
    {
        public virtual Guid Id { get; set; }
        public virtual String Nome { get; set; }
        public virtual int Desconto { get; set; }
        public virtual Boolean FoiUsado { get; set; }
        public virtual IList<Carrinho> Carrinhos { get; set; }

        public Cupom()
        {
            Carrinhos = new List<Carrinho>();
        }
    }

    public class CupomMap : ClassMapping<Cupom>
    {
        public CupomMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));
            Property(x => x.Nome);
            Property(x => x.Desconto);
            Property(x => x.FoiUsado);

            Bag(x => x.Carrinhos, m =>
            {
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
                m.Key(k => k.Column("CupomId"));
            }, r => r.OneToMany());
        }
    }
}
