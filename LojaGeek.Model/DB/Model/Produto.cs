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
        public virtual String Plataforma { get; set; }
        public virtual int Estoque { get; set; }
        public virtual double Preco { get; set; }
        public virtual DateTime DataModificacao { get; set; }
        public virtual String Foto { get; set; }
        public virtual Boolean Ativo { get; set; }
        public virtual IList<Comentario> Comentarios { get; set; }

        public Produto()
        {
            Comentarios = new List<Comentario>();
            DataModificacao = DateTime.Now;
        }


    }

    public class ProdutoMap : ClassMapping<Produto>
    {
        public ProdutoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));

            Property(x => x.Nome);
            Property(x => x.Descricao);
            Property(x => x.Plataforma);
            Property(x => x.Estoque);
            Property(x => x.Preco);
            Property(x => x.DataModificacao);
            Property(x => x.Ativo);
            Property(x => x.Foto);

            Bag(x => x.Comentarios, m =>
            {
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
                m.Key(k => k.Column("ProdutoId"));
            }, r => r.OneToMany());
        }
    }
}
