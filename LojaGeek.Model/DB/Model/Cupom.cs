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
        public virtual String Alias { get; set; }
        public virtual int Porcetagem { get; set; }
        public virtual Boolean FoiUsado { get; set; }
    }

    public class CupomMap : ClassMapping<Cupom>
    {
        public CupomMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));
            Property(x => x.Alias);
            Property(x => x.Porcetagem);
        }
    }
}
