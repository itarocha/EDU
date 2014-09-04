using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Visao360.Educacao.Models
{
    public class LogOnModel
    {
        [Required(ErrorMessage = "O Nome de Usuário é obrigatório"), MaxLength(64)]
        [Display(Name = "Nome de Usuário")]
        public string UserName { get; set; }

        [DataType(DataType.Password), MaxLength(32)]
        [Required(ErrorMessage = "A Senha de acesso é obrigatória.")]
        [StringLength(32, MinimumLength = 4)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
    }
}
