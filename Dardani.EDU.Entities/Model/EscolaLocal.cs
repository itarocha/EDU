using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class EscolaLocal
    {
        public virtual int Id { get; set; } 

        [Display(Name = "Escola")]
        [Required(ErrorMessage = "O campo Escola deve ser preenchido.")]
        public virtual Escola Escola { get; set; } 

        [Display(Name = "Local de funcionamento")]
        [Required(ErrorMessage = "O campo Local de funcionamento deve ser preenchido.")]
        public virtual LocalEscola LocalEscola { get; set; } 

    }
}
