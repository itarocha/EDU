using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class Calendario
    {
        public virtual int Id { get; set; }

        [Display(Name = "Escola")]
        [Required(ErrorMessage = "O campo Escola deve ser preenchido.")]
        public virtual Escola Escola { get; set; }

        [Required(ErrorMessage = "Ano Letivo precisa ser preenchido.")]
        [Display(Name = "Ano Letivo")]
        public virtual AnoLetivo AnoLetivo { get; set; }

        [Required(ErrorMessage = "Descrição precisa ser preenchida.")]
        [StringLength(32, MinimumLength = 3)]
        [Display(Name = "Descrição")]
        public virtual string Descricao { get; set; }

        [Required(ErrorMessage = "Data de Início precisa ser preenchida")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Início")]
        public virtual DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "Data de Término precisa ser preenchida")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Término")]
        public virtual DateTime DataTermino { get; set; }

        [Required(ErrorMessage = "Data de Resultado precisa ser preenchida")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Resultado")]
        public virtual DateTime DataResultado { get; set; }

        [Required(ErrorMessage = "Dias Letivos precisa ser preenchido.")]
        [Display(Name = "Dias Letivos")]
        public virtual int DiasLetivos { get; set; }

        public Calendario() {
            this.DataInicio = DateTime.Today;
            this.DataTermino = DateTime.Today;
            this.DataResultado = DateTime.Today;
        }
    }
}
