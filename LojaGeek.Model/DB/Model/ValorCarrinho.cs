using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Model.DB.Model
{
    public class ValorCarrinho
    {
        public virtual String Id { get; set; }
        public virtual String Cupom { get; set; }
        public virtual double SubTotal { get; set; }
        public virtual double Total { get; set; }

        public class ValorCarrinhoMap : ClassMapping<ValorCarrinho>
        {
            public ValorCarrinhoMap()
            {
                Id(x => x.Id);
                Property(x => x.Cupom);
                Property(x => x.SubTotal);
                Property(x => x.Total);

            }
        }
    }
}
