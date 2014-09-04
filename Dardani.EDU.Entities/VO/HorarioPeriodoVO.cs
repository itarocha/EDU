using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class HorarioPeriodoVO
    {
        public virtual int Id { get; set; }

        //[Required(ErrorMessage = "Hora Início precisa ser preenchida.")]
        //[StringLength(5, MinimumLength = 5)]
        [Display(Name = "Hora Início")]
        //[RegularExpression(@"^\d{2}:\d{2}$", ErrorMessage = "O Horário de Início deverá estar no formato 00:00")]
        //[ConverterEntidade]
        public virtual string HoraInicio { get; set; }

        //[Required(ErrorMessage = "Hora Término precisa ser preenchida.")]
        //[StringLength(5, MinimumLength = 5)]
        [Display(Name = "Hora Término")]
        //[RegularExpression(@"^\d{2}:\d{2}$", ErrorMessage = "O Horário de Término deverá estar no formato 00:00")]
        //[ConverterEntidade]
        public virtual string HoraTermino { get; set; }

        [Display(Name = "Horário")]
        [Required(ErrorMessage = "Horário precisa ser informado")]
        [ConverterEntidade(NomeEntidade = "Horario", Campo = "Horario")]
        public virtual int HorarioId { get; set; }

        public virtual string HorarioDescricao { get; set; }

        public virtual int HorarioSequencia { get; set; }

        [Display(Name = "Período")]
        [Required(ErrorMessage = "Período precisa ser informado")]
        [ConverterEntidade(NomeEntidade = "PeriodoAula", Campo = "PeriodoAula")] // Campo é opcional quando NomeEntidade preenchido
        public virtual int PeriodoAulaId { get; set; }

        public virtual string FaixaHorario {
            get {
                return string.Format("{0} - {1}",this.HoraInicio, this.HoraTermino);
            }
        }
    }
}
