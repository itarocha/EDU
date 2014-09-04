using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class EscolaLocalVO
    {
        public virtual int Id { get; set; }

        [Display(Name = "Escola")]
        [Required(ErrorMessage = "O campo Escola deve ser preenchido.")]
        public virtual int EscolaId { get; set; }

        [Display(Name = "Local de funcionamento")]
        [Required(ErrorMessage = "O campo Local de funcionamento deve ser preenchido.")]
        public virtual int LocalEscolaId { get; set; }

        public virtual int LocalEscolaDescricao { get; set; }
    }
}
