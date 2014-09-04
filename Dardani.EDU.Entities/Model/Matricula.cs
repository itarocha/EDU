using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class Matricula
    {
        public virtual int Id { get; set; }

        [Display(Name = "Aluno")]
        [Required(ErrorMessage = "O campo Aluno deve ser preenchido.")]
        public virtual Pessoa Pessoa { get; set; }

        [Display(Name = "Ano Letivo")]
        [Required(ErrorMessage = "O campo Ano Letivo deve ser preenchido.")]
        public virtual AnoLetivo AnoLetivo { get; set; }

        [Display(Name = "Turma")]
        [Required(ErrorMessage = "O campo Turma deve ser preenchido.")]
        public virtual Turma Turma { get; set; }

        [Display(Name = "Escolarização Especial")]
        [Required(ErrorMessage = "O campo Escolarização Especial deve ser preenchido.")]
        public virtual EscolarizacaoEspecial EscolarizacaoEspecial { get; set; }

        [Display(Name = "Transporte Público")]
        [Required(ErrorMessage = "O campo Transporte Público deve ser preenchido.")]
        public virtual TransportePublico TransportePublico { get; set; }

        [Display(Name = "Turma Unificada")]
        [Required(ErrorMessage = "O campo Turma Unificada deve ser preenchido.")]
        public virtual TurmaUnificada TurmaUnificada { get; set; }

        [Display(Name = "Rematricular")]
        [Required(ErrorMessage = "O campo Rematricular Letivo deve ser preenchido.")]
        public virtual string FlagRematricular { get; set; }
    }
}
