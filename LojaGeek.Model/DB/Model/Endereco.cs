using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Model.DB.Model
{
    public class Endereco
    {
        public static List<Endereco> Enderecos = new List<Endereco>();

        public virtual Guid Id { get; set; }
        public virtual Guid ClienteId { get; set; }
        public virtual String Descricao { get; set; }
        public virtual int Cep { get; set; }
        public virtual String Logradouro { get; set; }
        public virtual int Numero { get; set; }
        public virtual String Complemento { get; set; }
        public virtual String Bairro { get; set; }
        public virtual String Cidade { get; set; }
        public virtual String Uf { get; set; }
        public virtual Cliente Cliente { get; set; }
    }

    public class EnderecoMap : ClassMapping<Endereco>
    {
        public EnderecoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));

            Property(x => x.ClienteId);
            Property(x => x.Descricao);
            Property(x => x.Cep);
            Property(x => x.Logradouro);
            Property(x => x.Numero);
            Property(x => x.Complemento);
            Property(x => x.Bairro);
            Property(x => x.Cidade);
            Property(x => x.Uf);

            ManyToOne(x => x.Cliente, m => {
                m.Unique(true);
                m.Column("ClienteId");
                m.Lazy(LazyRelation.NoLazy);
                m.Cascade(Cascade.Persist);
            });
        }
    }
}
