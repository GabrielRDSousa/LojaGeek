using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Model.DB.Model
{
    public class Interesse
    {
        public virtual Guid Id { get; set; }
        public virtual String NomeInteresse { get; set; }
        public virtual Cliente Cliente { get; set; }
    }

    public class InteresseMap : ClassMapping<Interesse>
    {
        public InteresseMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));

            Property(x => x.NomeInteresse);

            ManyToOne(x => x.Cliente, m => {
                m.Column("ClienteId");
                m.Lazy(LazyRelation.NoLazy);
                m.Cascade(Cascade.Persist);
            });
        }
    }
}
