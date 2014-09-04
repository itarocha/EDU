using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class TurmaAEE
    {
        public virtual int Id { get; set; } 

        [Display(Name = "Turma")]
        [Required(ErrorMessage = "O campo Turma deve ser preenchido.")]
        public virtual Turma Turma { get; set; } 

        [Display(Name = "Item")]
        [Required(ErrorMessage = "O campo Item deve ser preenchido.")]
        public virtual TipoAEE TipoAEE { get; set; } 

    }
}
