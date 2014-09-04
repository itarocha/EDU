using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class HorarioPeriodo
    {
        public virtual int Id { get; set; }

        [Display(Name = "Horário")]
        [Required(ErrorMessage = "Horário precisa ser informado")]
        public virtual Horario Horario { get; set; }

        [Display(Name = "Período")]
        [Required(ErrorMessage = "Período precisa ser informado")]
        public virtual PeriodoAula PeriodoAula { get; set; }
    }
}
