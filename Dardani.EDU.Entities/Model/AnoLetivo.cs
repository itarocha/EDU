using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class AnoLetivo
    {
        public virtual int Id { get; set; }

        [Display(Name = "Ano")]
        [Required(ErrorMessage = "O campo Ano deve ser preenchido.")]
        public virtual int Ano { get; set; }

        [Display(Name = "Status?")]
        [Required(ErrorMessage = "O campo Status? deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        public virtual string FlagStatus { get; set; }

        public AnoLetivo() {
            this.Ano = DateTime.Today.Year;
        }
    }
}
