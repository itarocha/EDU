using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class Municipio
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Descrição precisa ser preenchida.")]
        [StringLength(128, MinimumLength = 3)]
        [Display(Name = "Descrição")]
        public virtual string Descricao { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "O campo Estado deve ser preenchido.")]
        public virtual Estado Estado { get; set; }

        [Display(Name = "Valor no Educacenso")]
        [Required(ErrorMessage = "O campo Valor no Educacenso deve ser preenchido.")]
        [StringLength(16, MinimumLength = 3)]
        public virtual string ValorEducacenso { get; set; }

        [Display(Name = "Ativo?")]
        [Required(ErrorMessage = "O campo Ativo? deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        public virtual string FlagAtivo { get; set; }
    }
}
