using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class CalendarioVO
    {

        public virtual int Id { get; set; }

        [Display(Name = "Escola")]
        [Required(ErrorMessage = "O campo Escola deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade="Escola", Campo="Escola")]
        public virtual int EscolaId { get; set; }

        public virtual string EscolaNome { get; set; }

        [Required(ErrorMessage = "Ano Letivo precisa ser preenchido.")]
        [Display(Name = "Ano Letivo")]
        [ConverterEntidade(NomeEntidade = "AnoLetivo", Campo = "AnoLetivo")]
        public virtual int AnoLetivoId { get; set; }

        public virtual int AnoLetivoAno { get; set; }

        [Required(ErrorMessage = "Descrição precisa ser preenchida.")]
        [StringLength(32, MinimumLength = 3)]
        [Display(Name = "Descrição")]
        [ConverterEntidade]
        public virtual string Descricao { get; set; }

        [Required(ErrorMessage = "Data de Início precisa ser preenchida")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Início")]
        [ConverterEntidade]
        public virtual DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "Data de Término precisa ser preenchida")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Término")]
        [ConverterEntidade]
        public virtual DateTime DataTermino { get; set; }

        [Required(ErrorMessage = "Data de Resultado precisa ser preenchida")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Resultado")]
        [ConverterEntidade]
        public virtual DateTime DataResultado { get; set; }

        [Required(ErrorMessage = "Dias Letivos precisa ser preenchido.")]
        [Display(Name = "Dias Letivos")]
        [ConverterEntidade]
        public virtual int DiasLetivos { get; set; }

        public CalendarioVO() {
            this.DataInicio = DateTime.Today;
            this.DataTermino = DateTime.Today;
            this.DataResultado = DateTime.Today;
        }

    }
}

