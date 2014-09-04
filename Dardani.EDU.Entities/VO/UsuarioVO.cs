using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class UsuarioVO
    {
        [ConverterEntidade]
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Nome precisa ser preenchido")]
        [ConverterEntidade]
        public virtual string Nome { get; set; }

        [Required(ErrorMessage = "Data de Nascimento precisa ser preenchido")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Nascimento")]
        [ConverterEntidade]
        public virtual DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "CPF precisa ser preenchido")]
        [StringLength(11, MinimumLength = 11)]
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "CPF Inválido")]
        [Display(Name = "Número do CPF")]
        [ConverterEntidade]
        public virtual string NumeroCPF { get; set; }

        [Required(ErrorMessage = "Sexo precisa ser informado")]
        [Display(Name = "Sexo")]
        [ConverterEntidade]
        public virtual int SexoId { get; set; }

        [Required(ErrorMessage = "Nível precisa ser preenchido.")]
        [Display(Name = "Nível")]
        [ConverterEntidade]
        public virtual string Nivel { get; set; } // super, administrador, cliente

        [Required(ErrorMessage = "Ativo precisa ser preenchido.")]
        [Display(Name = "Ativo")]
        [ConverterEntidade]
        public virtual string Ativo { get; set; }

        [Display(Name = "Situação")]
        public virtual string DescricaoAtivo { get { return (this.Ativo == "S") ? "Ativo" : "Inativo"; } }

        [Required(ErrorMessage = "Identificação precisa ser preenchido")]
        [StringLength(32, MinimumLength = 5)]
        [Display(Name = "Identificação")]
        [ConverterEntidade(Objeto="Acesso")]
        public virtual string NomeUsuario { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Senha de acesso precisa ser preenchida.")]
        [StringLength(32, MinimumLength = 5)]
        [Display(Name = "Senha")]
        [ConverterEntidade(Objeto = "Acesso")]
        public virtual string SenhaAcesso { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "E-mail precisa ser preenchido.")]
        [StringLength(64, MinimumLength = 8)]
        [Display(Name = "E-mail")]
        [ConverterEntidade(Objeto = "Acesso")]
        public virtual string Email { get; set; }

        public UsuarioVO()
        {
            Ativo = "S";
            //Sexo = "M";
            Nivel = "Visitante"; // Administrador/Visitante
        }
    }
}
