using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class Sala
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Descrição precisa ser preenchida.")]
        [StringLength(32, MinimumLength = 1)]
        [Display(Name = "Descrição")]
        public virtual string Descricao { get; set; }

        [Required(ErrorMessage = "Metragem precisa ser preenchida.")]
        public virtual decimal Metragem { get; set; }

        [Required(ErrorMessage = "Capacidade precisa ser preenchida.")]
        public virtual int Capacidade { get; set; }

        [Display(Name = "Escola")]
        [Required(ErrorMessage = "Escola precisa ser informada")]
        public virtual Escola Escola { get; set; }

        [Display(Name = "Tipo de Sala")]
        [Required(ErrorMessage = "Tipo de Sala precisa ser informado")]
        public virtual TipoSala TipoSala { get; set; }
    }
}
