using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class EtapaVO
    {
        public virtual int Id { get; set; }

        [Display(Name = "Código Modalidade")]
        public virtual int ModalidadeId { get; set; }

        [Display(Name = "Modalidade")]
        public virtual string ModalidadeDescricao { get; set; }

        [Display(Name = "Tipo de Ensino Id")]
        public virtual int TipoEnsinoId { get; set; }

        [Display(Name = "Tipo de Ensino")]
        public virtual string TipoEnsinoDescricao { get; set; }

        [Display(Name = "Série Id")]
        [Required(ErrorMessage = "O campo Série deve ser preenchido.")]
        public virtual int SerieId { get; set; }

        [Display(Name = "Série")]
        [Required(ErrorMessage = "O campo Série deve ser preenchido.")]
        public virtual string SerieDescricao { get; set; }

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
