using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class TipoDia
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Descrição precisa ser preenchida.")]
        [StringLength(64, MinimumLength = 3)]
        [Display(Name = "Descrição")]
        public virtual string Descricao { get; set; }

        [Required(ErrorMessage = "Letivo precisa ser preenchida.")]
        [StringLength(1)]
        [Display(Name = "Letivo")]
        public virtual string FlagLetivo { get; set; }

        [Required(ErrorMessage = "Cor precisa ser preenchida.")]
        [Display(Name = "Cor")]
        public virtual string Cor { get; set; }

        [Required(ErrorMessage = "Cor da Letra precisa ser preenchida.")]
        [Display(Name = "CorLetra")]
        public virtual string CorLetra { get; set; }

        public TipoDia() {
            Cor = "#FFFF00";
            CorLetra = "#000000";
        }
    }
}
