using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class Matriz
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Ano Letivo precisa ser informado.")]
        [Display(Name = "Ano Letivo")]
        public virtual AnoLetivo AnoLetivo { get; set; }

        [Required(ErrorMessage = "Modalidade precisa ser informada.")]
        [Display(Name = "Modalidade")]
        public virtual Modalidade Modalidade { get; set; }

        [Required(ErrorMessage = "Etapa precisa ser informada.")]
        [Display(Name = "Etapa")]
        public virtual Etapa Etapa { get; set; }

        [Required(ErrorMessage = "Dias Letivos precisa ser preenchido.")]
        [Display(Name = "Dias Letivos")]
        public virtual short DiasLetivos { get; set; } // 200

        [Required(ErrorMessage = "Carga Horária Semanal precisa ser preenchido.")]
        [Display(Name = "Carga Horária Semanal")]
        public virtual short CargaHorariaSemanal { get; set; } // 20 = 5 dias x 4 aulas dia

        [Required(ErrorMessage = "Carga Horária precisa ser preenchido.")]
        [Display(Name = "Carga Horária (minutos)")]
        public virtual short CargaHorariaAula { get; set; } // 50 minutos cada aula

        [Required(ErrorMessage = "Número de Semanas Letivas precisa ser preenchido.")]
        [Display(Name = "Número de Semanas")]
        public virtual short NumeroSemanasLetivas { get; set; } // 40 semanas letivas
    }
}
