using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class Escola
    {
        public virtual int Id { get; set; } 

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo Nome deve ser preenchido.")]
        [StringLength(128, MinimumLength = 4)]
        public virtual string Nome { get; set; } 

        [Display(Name = "Situação de Funcionamento")]
        [Required(ErrorMessage = "O campo Situação de Funcionamento deve ser preenchido.")]
        public virtual SituacaoFuncionamento SituacaoFuncionamento { get; set; } 

        [Display(Name = "Código do INEP")]
        public virtual string CodigoINEP { get; set; } 

        [Display(Name = "Nome do Gestor")]
        [Required(ErrorMessage = "O campo Nome do Gestor deve ser preenchido.")]
        [StringLength(64, MinimumLength = 4)]
        public virtual string NomeGestor { get; set; } 

        [Display(Name = "Gestor é Diretor?")]
        [Required(ErrorMessage = "O campo Gestor é Diretor? deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        public virtual string FlagGestorDiretor { get; set; } 

        [Display(Name = "E-mail")]
        [StringLength(64, MinimumLength = 4)]
        [DataType(DataType.EmailAddress)]        
        public virtual string Email { get; set; } 

        [Display(Name = "Código do Órgão Regional")]
        public virtual string CodigoOrgaoRegional { get; set; } 

        [Display(Name = "Nome do Órgão Regional")]
        public virtual string NomeOrgaoRegional { get; set; } 

        [Display(Name = "Dependência Administrativa")]
        [Required(ErrorMessage = "O campo Dependência Administrativa deve ser preenchido.")]
        public virtual TipoAdministracao TipoAdministracao { get; set; } 

        [Display(Name = "Dependência Administrativa")]
        [Required(ErrorMessage = "O campo Dependência Administrativa deve ser preenchido.")]
        public virtual EstagioRegulamentacao EstagioRegulamentacao { get; set; } 

        [Display(Name = "Número de Funcionários")]
        [Required(ErrorMessage = "O campo Número de Funcionários deve ser preenchido.")]
        public virtual int QuantidadeFuncionarios { get; set; } 

        [Display(Name = "Oferece Alimentação Escolar?")]
        [Required(ErrorMessage = "O campo Oferece Alimentação Escolar? deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        public virtual string FlagAlimentacaoEscolar { get; set; } 

    }
}
