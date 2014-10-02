using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class UsuarioAcao
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Usuário precisa ser informada.")]
        [Display(Name = "Usuário")]
        public virtual Usuario Usuario { get; set; }

        [Required(ErrorMessage = "Ação precisa ser informada.")]
        [Display(Name = "Ação")]
        public virtual Acao Acao { get; set; }
    }
}
