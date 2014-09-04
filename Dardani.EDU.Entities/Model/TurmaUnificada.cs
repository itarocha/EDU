using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class TurmaUnificada
    {
        public virtual int Id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O campo Descrição deve ser preenchido.")]
        [StringLength(64, MinimumLength = 3)]
        public virtual string Descricao { get; set; }

        [Display(Name = "Possui Turma Unificada?")]
        [Required(ErrorMessage = "O campo Possui Turma Unificada? deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        public virtual string FlagPossui { get; set; }

        [Display(Name = "Valor no Educacenso")]
        [Required(ErrorMessage = "O campo Valor no Educacenso deve ser preenchido.")]
        public virtual short ValorEducacenso { get; set; }

        [Display(Name = "Padrão?")]
        [Required(ErrorMessage = "O campo Padrão? deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        public virtual string FlagPadrao { get; set; }

        [Display(Name = "Ativo?")]
        [Required(ErrorMessage = "O campo Ativo? deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        public virtual string FlagAtivo { get; set; }

    }
}
