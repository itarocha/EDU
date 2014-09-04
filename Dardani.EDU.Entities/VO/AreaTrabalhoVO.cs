using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class AreaTrabalhoVO
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Descrição precisa ser preenchida.")]
        [StringLength(64, MinimumLength = 3)]
        [Display(Name = "Descrição")]
        [ConverterEntidade]
        public virtual string Descricao { get; set; }

        [Display(Name = "Ensino")]
        [Required(ErrorMessage = "Ensino precisa ser informado")]
        public virtual int EnsinoId { get; set; }

        public virtual string EnsinoDescricao { get; set; }

        public virtual string EnsinoDescricaoAbreviada { get; set; }
    }
}

