using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class EnsinoVO
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Descrição precisa ser preenchida.")]
        [StringLength(64, MinimumLength = 3)]
        [Display(Name = "Descrição")]
        [ConverterEntidade]
        public virtual string Descricao { get; set; }

        [Required(ErrorMessage = "Descrição Abreviada precisa ser preenchida.")]
        [StringLength(8, MinimumLength = 2)]
        [Display(Name = "Descrição Abreviada")]
        [ConverterEntidade]
        public virtual string DescricaoAbreviada { get; set; }

        [Display(Name = "Tipo de Ensino")]
        [Required(ErrorMessage = "Tipo de Ensino precisa ser informado")]
        [ConverterEntidade(NomeEntidade = "TipoEnsino", Campo="TipoEnsino")]
        public virtual int TipoEnsinoId { get; set; }

        public virtual string TipoEnsinoDescricao { get; set; }

        public virtual string TipoEnsinoDescricaoAbreviada { get; set; }
    }
}
