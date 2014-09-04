﻿using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class PessoaEnderecoVO
    {
        public virtual int Id { get; set; }

        [ConverterEntidade(NomeEntidade = "Pessoa")]
        public virtual int PessoaId { get; set; }

        public virtual int TurmaId { get; set; }

        //public virtual Pessoa Pessoa { get; set; }

        [Display(Name = "Logradouro")]
        [ConverterEntidade]
        public virtual string Logradouro { get; set; }

        [Display(Name = "Número")]
        [ConverterEntidade]
        public virtual short Numero { get; set; }

        [Display(Name = "Complemento")]
        [ConverterEntidade]
        public virtual string Complemento { get; set; }

        [Display(Name = "Bairro")]
        [ConverterEntidade]
        public virtual string Bairro { get; set; }

        [Display(Name = "CEP")]
        [StringLength(8)]
        //[RegularExpression(@"^\d{8}$|^\d{5}-\d{3}$", ErrorMessage = "O código postal deverá estar no formato 00000000 ou 00000-000")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "O código postal deverá estar no formato 00000-000")]
        [ConverterEntidade]
        public virtual string CEP { get; set; }

        [Display(Name = "Cidade")]
        [ConverterEntidade(NomeEntidade="Municipio", Campo="Cidade")]
        public virtual int CidadeId { get; set; }

        [Display(Name = "Estado")]
        [ConverterEntidade(NomeEntidade = "Estado", Campo="UF")]
        public virtual int UFId { get; set; }

        [Display(Name = "Zona")]
        [ConverterEntidade(NomeEntidade = "Zona")]
        public virtual int ZonaId { get; set; }

        public virtual string ZonaDescricao { get; set; }

        [Display(Name = "Telefone")]
        [ConverterEntidade]
        public virtual string Telefone { get; set; }

        [Display(Name = "Fax")]
        [ConverterEntidade]
        public virtual string Fax { get; set; }

        [Display(Name = "E-mail")]
        [ConverterEntidade]
        public virtual string Email { get; set; } 

    }
}