using LojaGeek.Model.DB.Model;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Model.DB
{
    public class HistoricoDeBusca
    {
        public virtual Guid Id { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual String Pesquisa { get; set; }
        public virtual DateTime Data { get; set; }

        public HistoricoDeBusca()
        {
            Data = DateTime.Now;
        }
    }

    public class HistoricoDeBuscaMap : ClassMapping<HistoricoDeBusca>
    {
        public HistoricoDeBuscaMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));
            Property(x => x.Pesquisa);
            Property(x => x.Data);

            ManyToOne(x => x.Cliente, m => {
                m.Column("ClienteId");
                m.Lazy(LazyRelation.NoLazy);
                m.Cascade(Cascade.Refresh);
            });
        }
    }
}
