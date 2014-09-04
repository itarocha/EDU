using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class EscolaPrivadaMantenedor
    {
        public virtual int Id { get; set; } 

        [Display(Name = "Escola")]
        [Required(ErrorMessage = "O campo Escola deve ser preenchido.")]
        public virtual Escola Escola { get; set; } 

        [Display(Name = "Mantenedor Privado")]
        [Required(ErrorMessage = "O campo Mantenedor Privado deve ser preenchido.")]
        public virtual MantenedorPrivado MantenedorPrivado { get; set; } 

    }
}
