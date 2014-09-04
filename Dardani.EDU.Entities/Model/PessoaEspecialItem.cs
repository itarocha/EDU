using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class PessoaEspecialItem
    {
        public virtual int Id { get; set; }

        [Display(Name = "Pessoa")]
        [Required(ErrorMessage = "O campo Pessoa deve ser preenchido.")]
        public virtual Pessoa Pessoa { get; set; }

        [Display(Name = "Item")]
        [Required(ErrorMessage = "O campo Item deve ser preenchido.")]
        public virtual EspecialItem EspecialItem { get; set; }

    }
}
