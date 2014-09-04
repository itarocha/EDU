using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Visao360.Educacao.Models
{
    public class EscolaSessao
    {
        [Required(ErrorMessage = "Escola é campo obrigatório.")]
        [Display(Name = "Escola")]
        public int EscolaId { get; set; }

        public string EscolaNome { get; set; }

        [Required(ErrorMessage = "Ano Letivo é campo obrigatório.")]
        [Display(Name = "Ano Letivo")]
        public int AnoLetivoId { get; set; }

        public int AnoLetivoAno { get; set; }
    }
}