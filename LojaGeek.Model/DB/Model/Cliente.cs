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
        [Required(ErrorMessage = "Informe o Nome")]
        public virtual String Nome { get; set; }
        [Required(ErrorMessage = "Informe o Sobrenome")]
        public virtual String Sobrenome { get; set; }
        [Required(ErrorMessage = "Informe o CPF")]
        [MaxLength(11, ErrorMessage = "Cpf inválido")]
        [MinLength(11, ErrorMessage = "Cpf inválido")]
        [Cpf]
        public virtual String Cpf { get; set; }
        [Required(ErrorMessage = "Informe o E-mail")]
        [EmailAddress]
        public virtual String Email { get; set; }
        public virtual String Senha { get; set; }
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

        }
    }
}
