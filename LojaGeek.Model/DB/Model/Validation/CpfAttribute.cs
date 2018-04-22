using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Model.DB.Model.Validation
{
    public class CpfAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                String cpf = Convert.ToString(value);
                var cliente = DbFactory.Instance.ClienteRepository.FindByCpf(cpf);
                if (cliente != null)
                    return new ValidationResult("Já existe um cliente com esse CPF");
            }
            return ValidationResult.Success;
        }
    }
}
