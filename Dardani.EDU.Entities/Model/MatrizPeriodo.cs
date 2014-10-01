using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class MatrizPeriodo
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Matriz Curricular precisa ser informada.")]
        [Display(Name = "Matriz Curricular")]
        public virtual Matriz Matriz { get; set; }

        [Required(ErrorMessage = "Período precisa ser informado.")]
        [Display(Name = "Período")]
        public virtual PeriodoAno PeriodoAno { get; set; }

        [Required(ErrorMessage = "Data de Início precisa ser preenchida")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Início")]
        public virtual DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "Data de Término precisa ser preenchida")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Término")]
        public virtual DateTime DataTermino { get; set; }
    }
}
