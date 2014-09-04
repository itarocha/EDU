using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class UsuarioAcesso
    {
        public virtual int Id { get; set; }

        public virtual Usuario Usuario { get; set; }

        [Required(ErrorMessage = "Identificação precisa ser preenchido")]
        [StringLength(32, MinimumLength = 5)]
        [Display(Name = "Identificação")]
        public virtual string NomeUsuario { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Senha de acesso precisa ser preenchida.")]
        [StringLength(32, MinimumLength = 5)]
        [Display(Name = "Senha")]
        public virtual string SenhaAcesso { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "E-mail precisa ser preenchido.")]
        [StringLength(64, MinimumLength = 8)]
        [Display(Name = "E-mail")]
        public virtual string Email { get; set; }

    }
}