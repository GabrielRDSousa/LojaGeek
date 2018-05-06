using LojaGeek.Model.DB.Model.Validation;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LojaGeek.Model.DB.Model
{
    public class Cliente
    {
        public static List<Cliente> Clientes = new List<Cliente>();

        public virtual Guid Id { get; set; }
        public virtual String Nome { get; set; }
        public virtual String Sobrenome { get; set; }
        public virtual String Cpf { get; set; }
        public virtual String Email { get; set; }
        public virtual String Senha { get; set; }
        public virtual IList<Endereco> Enderecos { get; set; }
        public virtual IList<Interesse> Interesses { get; set; }
        public virtual IList<Compra> Compras { get; set; }

        public Cliente()
        {
            Enderecos = new List<Endereco>();
            Interesses = new List<Interesse>();
            Compras = new List<Compra>();
        }
    }

    public class ClienteMap : ClassMapping<Cliente>
    {
        public ClienteMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));

            Property(x => x.Nome);
            Property(x => x.Sobrenome);
            Property(x => x.Cpf);
            Property(x => x.Email);
            Property(x => x.Senha);

            Bag(x => x.Enderecos, m =>
            {
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
                m.Key(k => k.Column("ClienteId"));
            }, r => r.OneToMany());

            Bag(x => x.Interesses, m =>
            {
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
                m.Key(k => k.Column("ClienteId"));
            }, r => r.OneToMany());

            Bag(x => x.Compras, m =>
            {
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
                m.Key(k => k.Column("ClienteId"));
            }, r => r.OneToMany());
        }
    }
}
