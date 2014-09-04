using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class MatriculaVeiculoTipo
    {
        public virtual int Id { get; set; }

        [Display(Name = "Matrícula")]
        [Required(ErrorMessage = "O campo Matricula deve ser preenchido.")]
        public virtual Matricula Matricula { get; set; }

        [Display(Name = "Veículo")]
        [Required(ErrorMessage = "O campo Veículo deve ser preenchido.")]
        public virtual VeiculoTipo VeiculoTipo { get; set; }
    }
}
