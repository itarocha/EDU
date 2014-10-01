using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class ConceitoNivelVO
    {
        public virtual int Id { get; set; }

        [Display(Name = "Conceito")]
        [Required(ErrorMessage = "O campo Conceito deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "Conceito", Campo = "Conceito")]
        public virtual int ConceitoId { get; set; }

        public virtual string ConceitoDescricao { get; set; }

        [Required(ErrorMessage = "Descrição precisa ser preenchida.")]
        [StringLength(32, MinimumLength = 1)]
        [Display(Name = "Descrição")]
        [ConverterEntidade]
        public virtual string Descricao { get; set; }

        [Display(Name = "Peso")]
        [Required(ErrorMessage = "O campo Peso deve ser preenchido.")]
        [ConverterEntidade]
        public virtual short Peso { get; set; }

        public ConceitoNivelVO() {
            this.Peso = -1;
        }
    }
}