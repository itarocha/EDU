using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class TurmaComplementar
    {
        public virtual int Id { get; set; } 

        [Display(Name = "Turma")]
        [Required(ErrorMessage = "O campo Turma deve ser preenchido.")]
        public virtual Turma Turma { get; set; } 

        [Display(Name = "Atividade Complementar")]
        [Required(ErrorMessage = "O campo Atividade Complementar deve ser preenchido.")]
        public virtual TipoComplementar TipoComplementar { get; set; } 

    }
}
