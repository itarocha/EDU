using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class PessoaEndereco
    {
        public virtual int Id { get; set; } 

        public virtual Pessoa Pessoa { get; set; } 

        [Display(Name = "Logradouro")]
        public virtual string Logradouro { get; set; } 

        [Display(Name = "Número")]
        public virtual short Numero { get; set; } 

        [Display(Name = "Complemento")]
        public virtual string Complemento { get; set; } 

        [Display(Name = "Bairro")]
        public virtual string Bairro { get; set; } 

        [Display(Name = "CEP")]
        public virtual string CEP { get; set; } 

        [Display(Name = "Cidade")]
        public virtual Municipio Cidade { get; set; } 

        [Display(Name = "Estado")]
        public virtual Estado UF { get; set; } 

        [Display(Name = "Zona")]
        public virtual Zona Zona { get; set; } 

        [Display(Name = "Telefone")]
        public virtual string Telefone { get; set; } 

        [Display(Name = "Fax")]
        public virtual string Fax { get; set; } 

        [Display(Name = "E-mail")]
        public virtual string Email { get; set; } 

    }
}
