﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class Usuario
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Nome precisa ser preenchido")]
        public virtual string Nome { get; set; }

        [Required(ErrorMessage = "Data de Nascimento precisa ser preenchido")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Nascimento")]
        public virtual DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "CPF precisa ser preenchido")]
        [StringLength(11, MinimumLength = 11)]
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "CPF Inválido")]
        [Display(Name = "Número do CPF")]
        public virtual string NumeroCPF { get; set; }

        [Required(ErrorMessage = "Sexo precisa ser informado")]
        [Display(Name = "Sexo")]
        public virtual Sexo Sexo { get; set; }

        [Required(ErrorMessage = "Nível precisa ser preenchido.")]
        [Display(Name = "Nível")]
        public virtual string Nivel { get; set; } // super, administrador, cliente

        [Required(ErrorMessage = "Ativo precisa ser preenchido.")]
        [Display(Name = "Ativo")]
        public virtual string Ativo { get; set; }

        [Display(Name = "Situação")]
        public virtual string DescricaoAtivo { get { return (this.Ativo == "S") ? "Ativo" : "Inativo"; } }

        public virtual UsuarioAcesso Acesso { get; set; }

        public Usuario()
        {
            Ativo = "S";
            //Sexo = "M";
            Nivel = "Visitante"; // Administrador/Visitante
            Acesso = new UsuarioAcesso();
        }
    }
}
