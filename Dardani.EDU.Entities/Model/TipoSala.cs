using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class TipoSala
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Descrição precisa ser preenchida.")]
        [StringLength(64, MinimumLength = 3)]
        [Display(Name = "Descrição")]
        public virtual string Descricao { get; set; }

        [Required(ErrorMessage = "Sala de Aula precisa ser preenchida.")]
        [StringLength(1)]
        [Display(Name = "Sala de Aula")]
        public virtual string FlagSalaAula { get; set; }
    }
}
