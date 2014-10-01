using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class MatrizPeriodoDisciplinaVO
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

        [Required(ErrorMessage = "Disciplina precisa ser informada.")]
        [Display(Name = "Disciplina")]
        [ConverterEntidade(NomeEntidade = "Disciplina", Campo = "Disciplina")]
        public virtual int DisciplinaId { get; set; }

        public virtual int DisciplinaDescricao { get; set; }

        public virtual int DisciplinaDescricaoAbreviada { get; set; }

        // Requerido se FlagTipoAvaliacao = "C". Deve ser um elemento de Conceito
        [Display(Name = "Nível Mínimo para Aprovação")]
        public virtual int ConceitoNivelMinimoId { get; set; }

        public virtual string ConceitoNivelMinimoDescricao { get; set; }

        // Requerido se FlagTipoAvaliacao = "N"
        [Display(Name = "Nota Maxima")]
        public virtual decimal NotaMaxima { get; set; }

        // Requerido se FlagTipoAvaliacao = "N"
        [Display(Name = "Nota Mínima para Aprovação")]
        public virtual decimal NotaMinima { get; set; }
    }
}
