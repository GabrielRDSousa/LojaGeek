using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Model.DB.Model
{
    public class Produto
    {
        public static List<Produto> Produtos = new List<Produto>();

        public virtual Guid Id { get; set; }
        public virtual String Nome { get; set; }
        public virtual String Descricao { get; set; }
        public virtual int QuantidadeEstoque { get; set; }
        public virtual double PrecoUnitario { get; set; }
        public virtual String Foto { get; set; }
    }

    public class ProdutoMap : ClassMapping<Produto>
    {
        public ProdutoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));

            Property(x => x.Nome);
            Property(x => x.Descricao);
            Property(x => x.QuantidadeEstoque);
            Property(x => x.PrecoUnitario);
            Property(x => x.Foto);
        }
    }
}
