using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class CalendarioDia
    {
        public virtual int Id { get; set; }

        [Display(Name = "Turma")]
        [Required(ErrorMessage = "O campo Calendário deve ser preenchido.")]
        public virtual Calendario Calendario { get; set; } 

        [Display(Name = "Tipo de Dia")]
        [Required(ErrorMessage = "O campo Tipo de Dia deve ser preenchido.")]
        public virtual TipoDia TipoDia { get; set; } 

        [Display(Name = "Data do Evento")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public virtual DateTime DataEvento { get; set; } 
    }
}
