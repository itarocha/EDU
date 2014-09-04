using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class PessoaRecursoINEP
    {
        public virtual int Id { get; set; }

        [Display(Name = "Pessoa")]
        [Required(ErrorMessage = "O campo Pessoa deve ser preenchido.")]
        public virtual Pessoa Pessoa { get; set; }

        [Display(Name = "Recurso INEP")]
        [Required(ErrorMessage = "O campo Recurso INEP deve ser preenchido.")]
        public virtual RecursoINEP RecursoINEP { get; set; }

    }
}
