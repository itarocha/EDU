using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class EscolaInfraestruturaItemVO
    {
        public virtual int Id { get; set; }

        [Display(Name = "Escola")]
        [Required(ErrorMessage = "O campo Escola deve ser preenchido.")]
        public virtual int EscolaId { get; set; }

        [Display(Name = "Item")]
        [Required(ErrorMessage = "O campo Item deve ser preenchido.")]
        public virtual int InfraestruturaItemId { get; set; }

        public virtual string InfraestruturaItemDescricao { get; set; }
    }
}
