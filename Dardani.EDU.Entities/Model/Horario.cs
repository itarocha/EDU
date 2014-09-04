using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class Horario
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Descrição precisa ser preenchida.")]
        [StringLength(64, MinimumLength = 3)]
        [Display(Name = "Descrição")]
        public virtual string Descricao { get; set; }

        [Display(Name = "Hora Inicial")]
        [StringLength(8, MinimumLength = 4)]
        public virtual string HoraInicial { get; set; }

        [Display(Name = "Hora Final")]
        [StringLength(8, MinimumLength = 4)]
        public virtual string HoraFinal { get; set; }
    }
}
