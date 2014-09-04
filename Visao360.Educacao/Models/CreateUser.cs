using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Visao360.Educacao.Models
{
    public class CreateUser 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome precisa ser preenchido"), MaxLength(64)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Data de Nascimento precisa ser preenchido")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Sexo precisa ser preenchido"), MaxLength(1)]
        [Display(Name = "Sexo")]
        public string Sexo { get; set; }

        [MaxLength(32), Required(ErrorMessage = "Nível precisa ser preenchido.")]
        [Display(Name = "Nível")]
        public string Nivel { get; set; } // super, administrador, cliente

        [MaxLength(1)]
        [Required(ErrorMessage = "Ativo precisa ser preenchido.")]
        [Display(Name = "Ativo")]
        public string Ativo { get; set; }

        [Display(Name = "Situação")]
        public string DescricaoAtivo { get { return (this.Ativo == "S") ? "Ativo" : "Inativo"; } }

        [Display(Name = "Acesso")]
        public int UsuarioAcessoId { get; set; }

        [Display(Name = "Identidade")]
        public int UsuarioRGId { get; set; }

        [Display(Name = "CPF")]
        public int UsuarioCPFId { get; set; }

        [Display(Name = "Endereco")]
        public int UsuarioEnderecoId { get; set; }

        //public virtual UsuarioAcesso Acesso { get; set; }

        //public virtual UsuarioCPF CPF { get; set; }

        //public virtual UsuarioRG RG { get; set; }

        //public virtual UsuarioEndereco Endereco { get; set; }

        public CreateUser()
        {
            Ativo = "S";
            Sexo = "M";
            Nivel = "Cliente";
            DataNascimento = DateTime.MinValue;
        }


        [MaxLength(32)]
        [StringLength(32, MinimumLength = 5)]
        [Display(Name = "Identificação")]
        public string NomeUsuario { get; set; }

        [DataType(DataType.Password), MaxLength(32)]
        [StringLength(32, MinimumLength = 6)]
        [Display(Name = "Senha")]
        public string SenhaAcesso { get; set; }

        [DataType(DataType.EmailAddress), MaxLength(32)]
        [StringLength(64, MinimumLength = 8)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "CPF precisa ser preenchido"), MaxLength(16)]
        [StringLength(11, MinimumLength = 11)]
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "CPF Inválido")]
        [Display(Name = "Número do CPF")]
        public string NumeroCPF { get; set; }

        [Required(ErrorMessage = "Carteira de Identidade precisa ser preenchida"), MaxLength(32)]
        [StringLength(32, MinimumLength = 8)]
        [Display(Name = "Carteira de Identidade")]
        public string NumeroRG { get; set; }
    }
}