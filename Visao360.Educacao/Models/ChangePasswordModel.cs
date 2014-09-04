using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Visao360.Educacao.Models
{
    public class ChangePasswordModel
    {
        [DataType(DataType.Password), MaxLength(32)]
        [Required(ErrorMessage = "A Senha anterior é obrigatória.")]
        [StringLength(32, MinimumLength = 8)]
        [Display(Name = "Senha Anterior")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password), MaxLength(32)]
        [Required(ErrorMessage = "A nova senha de acesso é obrigatória.")]
        [StringLength(32, MinimumLength = 8)]
        [Display(Name = "Nova Senha")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password), MaxLength(32)]
        [Required(ErrorMessage = "A Senha de confirmação acesso é obrigatória.")]
        [StringLength(32, MinimumLength = 8)]
        [Display(Name = "Senha de Confirmação")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "A senha de confirmação deve ser idêntica a nova senha")]
        public string PasswordConfirmation { get; set; }
    
    }
}
