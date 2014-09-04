using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class EscolaTurno
    {
        public virtual int Id { get; set; } 

        [Display(Name = "Escola")]
        [Required(ErrorMessage = "O campo Escola deve ser preenchido.")]
        public virtual Escola Escola { get; set; } 

        [Display(Name = "Turno")]
        [Required(ErrorMessage = "O campo Turno deve ser preenchido.")]
        public virtual Turno Turno { get; set; } 
    }
}
