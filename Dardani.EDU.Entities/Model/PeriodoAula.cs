using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class PeriodoAula
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Hora Início precisa ser preenchida.")]
        [StringLength(5, MinimumLength = 5)]
        [Display(Name = "Hora Início")]
        public virtual string HoraInicio { get; set; }

        [Required(ErrorMessage = "Hora Término precisa ser preenchida.")]
        [StringLength(5, MinimumLength = 5)]
        [Display(Name = "Hora Término")]
        public virtual string HoraTermino { get; set; }

    }
}
