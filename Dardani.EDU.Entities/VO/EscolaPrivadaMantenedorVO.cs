using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class EscolaPrivadaMantenedorVO
    {
        public virtual int Id { get; set; }

        [Display(Name = "Escola")]
        [Required(ErrorMessage = "O campo Escola deve ser preenchido.")]
        public virtual int EscolaId { get; set; }

        [Display(Name = "Mantenedor Privado")]
        [Required(ErrorMessage = "O campo Mantenedor Privado deve ser preenchido.")]
        public virtual int MantenedorPrivadoId { get; set; }

        public virtual string MantenedorPrivadoDescricao { get; set; }
    }
}
