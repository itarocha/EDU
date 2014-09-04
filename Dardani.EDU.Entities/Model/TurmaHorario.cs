using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class TurmaHorario
    {
        public virtual int Id { get; set; }
        
        [Required(ErrorMessage = "Turma precisa ser informado.")]
        [Display(Name = "Turma")]
        public virtual Turma Turma { get; set; }

        [Required(ErrorMessage = "Período precisa ser informada.")]
        [Display(Name = "Período")]
        public virtual PeriodoAula PeriodoAula { get; set; }

        [Required(ErrorMessage = "Disciplina precisa ser informada.")]
        [Display(Name = "Disciplina")]
        public virtual Disciplina Disciplina { get; set; }

        [Required(ErrorMessage = "Profissional precisa ser preenchido.")]
        [Display(Name = "Profissional")]
        public virtual Pessoa Pessoa { get; set; } // 200

        [Required(ErrorMessage = "Dia da Semana precisa ser preenchido.")]
        [Display(Name = "Dia da Semana")]
        public virtual short FlagDiaSemana { get; set; } // 20 = 5 dias x 4 aulas dia
    }
}
