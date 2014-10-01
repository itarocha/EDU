using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class ConceitoNivel
    {
        public virtual int Id { get; set; }

        [Display(Name = "Conceito")]
        [Required(ErrorMessage = "O campo Conceito deve ser preenchido.")]
        public virtual Conceito Conceito { get; set; }

        [Required(ErrorMessage = "Descrição precisa ser preenchida.")]
        [StringLength(32, MinimumLength = 1)]
        [Display(Name = "Descrição")]
        public virtual string Descricao { get; set; }

        [Display(Name = "Peso")]
        [Required(ErrorMessage = "O campo Peso deve ser preenchido.")]
        public virtual short Peso { get; set; }

        public ConceitoNivel() {
            this.Peso = -1;
        }
    }
}
