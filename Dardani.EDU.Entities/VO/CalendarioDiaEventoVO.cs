using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petra.Util.Attributes;

namespace Dardani.EDU.Entities.VO
{
    public class CalendarioDiaEventoVO
    {
        public virtual int Id { get; set; }

        [Display(Name = "Calendário")]
        [Required(ErrorMessage = "O campo Calendario deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "Calendario")]
        public virtual int CalendarioId { get; set; } 

        [Display(Name = "Tipo de Evento")]
        [Required(ErrorMessage = "O campo Tipo de Evento deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "TipoEvento")]
        public virtual int TipoEventoId { get; set; } 

        public virtual string TipoEventoDescricao { get; set; }

        public virtual string TipoEventoSimbolo { get; set; }

        [Display(Name = "Data do Evento")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [ConverterEntidade]
        public virtual DateTime DataEvento { get; set; } 
    }
}
