using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class MatrizPeriodoDisciplina
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Matriz Curricular precisa ser informada.")]
        [Display(Name = "Matriz Curricular")]
        public virtual Matriz Matriz { get; set; }

        [Required(ErrorMessage = "Período precisa ser informado.")]
        [Display(Name = "Período")]
        public virtual PeriodoAno PeriodoAno { get; set; }

        [Required(ErrorMessage = "Disciplina precisa ser informada.")]
        [Display(Name = "Disciplina")]
        public virtual Disciplina Disciplina { get; set; }

        // Requerido se FlagTipoAvaliacao = "C". Deve ser um elemento de Conceito
        [Display(Name = "Nível Mínimo para Aprovação")]
        public virtual ConceitoNivel ConceitoNivelMinimo { get; set; }

        // NEW - Indica a Nota Máxima (Valor do Período) 
        // Requerido se FlagTipoAvaliacao = "N"
        [Display(Name = "Nota Maxima")]
        public virtual decimal NotaMaxima { get; set; }

        // NEW - Indica a Nota Mínima
        // Requerido se FlagTipoAvaliacao = "N"
        [Display(Name = "Nota Mínima para Aprovação")]
        public virtual decimal NotaMinima { get; set; }
    }
}
