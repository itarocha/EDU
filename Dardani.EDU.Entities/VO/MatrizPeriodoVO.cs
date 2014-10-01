using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class MatrizPeriodoVO
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Matriz precisa ser informada.")]
        [Display(Name = "Matriz")]
        [ConverterEntidade(NomeEntidade = "Matriz", Campo = "Matriz")]
        public virtual int MatrizId { get; set; }


        [Required(ErrorMessage = "Período precisa ser informado.")]
        [Display(Name = "Período")]
        [ConverterEntidade(NomeEntidade = "PeriodoAno", Campo = "PeriodoAno")]
        public virtual int PeriodoAnoId { get; set; }

        public virtual int PeriodoAnoDescricao { get; set; }

        public virtual int PeriodoAnoDescricaoAbreviada { get; set; }


        [Required(ErrorMessage = "Data de Início precisa ser preenchida")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Início")]
        public virtual DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "Data de Término precisa ser preenchida")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Término")]
        public virtual DateTime DataTermino { get; set; }
    }
}
