using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class TipoEvento
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Descrição precisa ser preenchida.")]
        [StringLength(64, MinimumLength = 3)]
        [Display(Name = "Descrição")]
        public virtual string Descricao { get; set; }

        [Required(ErrorMessage = "Símbolo precisa ser preenchida.")]
        [StringLength(32)]
        [ManterCase]
        [Display(Name = "Símbolo")]
        public virtual string Simbolo { get; set; }

        [Required(ErrorMessage = "Dia Escolar precisa ser preenchido.")]
        [StringLength(1)]
        [Display(Name = "Dia Escolar?")]
        public virtual string FlagEscolar { get; set; }
    }
}
