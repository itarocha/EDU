using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class Etapa
    {
        public virtual int Id { get; set; } 

        [Display(Name = "Modalidade")]
        [Required(ErrorMessage = "O campo Modalidade deve ser preenchido.")]
        public virtual Modalidade Modalidade { get; set; } 

        [Display(Name = "TipoEnsino")]
        [Required(ErrorMessage = "O campo TipoEnsino deve ser preenchido.")]
        public virtual TipoEnsino TipoEnsino { get; set; } 

        [Display(Name = "Série")]
        [Required(ErrorMessage = "O campo Série deve ser preenchido.")]
        public virtual Serie Serie { get; set; } 

        [Display(Name = "Sequência")]
        public virtual short Sequencia { get; set; } 

        [Display(Name = "Valor no Educacenso")]
        [Required(ErrorMessage = "O campo Valor no Educacenso deve ser preenchido.")]
        public virtual short ValorEducacenso { get; set; } 

        [Display(Name = "Ativo?")]
        [Required(ErrorMessage = "O campo Ativo? deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        public virtual string FlagAtivo { get; set; } 

    }
}
