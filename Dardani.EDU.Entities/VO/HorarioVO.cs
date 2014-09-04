using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class HorarioVO
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Descrição precisa ser preenchida.")]
        [StringLength(64, MinimumLength = 3)]
        [Display(Name = "Descrição")]
        [ConverterEntidade]
        public virtual string Descricao { get; set; }

        [Display(Name = "Hora Inicial")]
        [StringLength(5)]
        [RegularExpression(@"^\d{2}:\d{2}$", ErrorMessage = "A Hora Inicial deverá estar no formato 00:00")]
        [ConverterEntidade]
        public virtual string HoraInicial { get; set; }

        [Display(Name = "Hora Final")]
        [StringLength(5)]
        [RegularExpression(@"^\d{2}:\d{2}$", ErrorMessage = "A Hora Final deverá estar no formato 00:00")]
        [ConverterEntidade]
        public virtual string HoraFinal { get; set; }

    }
}
