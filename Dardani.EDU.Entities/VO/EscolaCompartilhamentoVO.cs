using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class EscolaCompartilhamentoVO
    {
        public virtual int Id { get; set; }

        [Display(Name = "Escola")]
        [Required(ErrorMessage = "O campo Escola deve ser preenchido.")]
        public virtual int EscolaId { get; set; }

        [Display(Name = "Local de Funcionamento")]
        [Required(ErrorMessage = "O campo Local de Funcionamento deve ser preenchido.")]
        public virtual int EscolaCompartilhadaId { get; set; }

        public virtual string EscolaCompartilhadaNome { get; set; }

    }
}
