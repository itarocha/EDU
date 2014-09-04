using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class EscolaVO
    {
        public virtual int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo Nome deve ser preenchido.")]
        [StringLength(128, MinimumLength = 4)]
        [ConverterEntidade]
        public virtual string Nome { get; set; }

        [Display(Name = "Situação de Funcionamento")]
        [Required(ErrorMessage = "O campo Situação de Funcionamento deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade="SituacaoFuncionamento",Campo="SituacaoFuncionamento")] // Campo é opcional quando NomeEntidade preenchido
        public virtual int SituacaoFuncionamentoId { get; set; }

        public virtual string SituacaoFuncionamentoDescricao { get; set; }

        [Display(Name = "Código do INEP")]
        [StringLength(8, MinimumLength = 8)]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "O código INEP deverá estar no formato 00000000")]
        [ConverterEntidade]
        public virtual string CodigoINEP { get; set; }

        [Display(Name = "Nome do Gestor")]
        [Required(ErrorMessage = "O campo Nome do Gestor deve ser preenchido.")]
        [StringLength(64, MinimumLength = 4)]
        [ConverterEntidade]
        public virtual string NomeGestor { get; set; }

        [Display(Name = "Gestor é Diretor?")]
        [Required(ErrorMessage = "O campo Gestor é Diretor? deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagGestorDiretor { get; set; }

        [Display(Name = "E-mail")]
        [StringLength(64, MinimumLength = 4)]
        [DataType(DataType.EmailAddress)]
        [ConverterEntidade]
        public virtual string Email { get; set; }

        [Display(Name = "Código do Órgão Regional")]
        [ConverterEntidade]
        public virtual string CodigoOrgaoRegional { get; set; }

        [Display(Name = "Nome do Órgão Regional")]
        [ConverterEntidade]
        public virtual string NomeOrgaoRegional { get; set; }

        [Display(Name = "Dependência Administrativa")]
        [Required(ErrorMessage = "O campo Dependência Administrativa deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade="TipoAdministracao")]
        public virtual int TipoAdministracaoId { get; set; }

        public virtual string TipoAdministracaoDescricao { get; set; }

        [Display(Name = "Estágio da Regulamentação")]
        [Required(ErrorMessage = "O Estágio da Regulamentação deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "EstagioRegulamentacao")]
        public virtual int EstagioRegulamentacaoId { get; set; }

        public virtual string EstagioRegulamentacaoDescricao { get; set; }

        [Display(Name = "Número de Funcionários")]
        [Required(ErrorMessage = "O campo Número de Funcionários deve ser preenchido.")]
        [ConverterEntidade]
        public virtual int QuantidadeFuncionarios { get; set; }

        [Display(Name = "Oferece Alimentação Escolar?")]
        [Required(ErrorMessage = "O campo Oferece Alimentação Escolar? deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagAlimentacaoEscolar { get; set; }
    }
}
