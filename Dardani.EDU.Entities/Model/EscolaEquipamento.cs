using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class EscolaEquipamento
    {
        public virtual int Id { get; set; } 

        [Display(Name = "Escola")]
        [Required(ErrorMessage = "O campo Escola deve ser preenchido.")]
        public virtual Escola Escola { get; set; } 

        [Display(Name = "Item")]
        [Required(ErrorMessage = "O campo Item deve ser preenchido.")]
        public virtual Equipamento Equipamento { get; set; } 

        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "O campo Quantidade deve ser preenchido.")]
        public virtual short Quantidade { get; set; } 

    }
}
