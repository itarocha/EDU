using Dardani.EDU.Entities.Model;
using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class PessoaVO
    {
        public PessoaVO() {
            DataNascimento = DateTime.Now;
        }

        public virtual int Id { get; set; }

        public virtual int PessoaId { get; set; }

        public virtual int TurmaId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo Nome deve ser preenchido.")]
        [StringLength(64, MinimumLength = 4)]
        [ConverterEntidade]
        public virtual string Nome { get; set; }

        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "O campo Data de Nascimento deve ser preenchido.")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [ConverterEntidade]
        public virtual DateTime DataNascimento { get; set; }

        [Display(Name = "Sexo")]
        [Required(ErrorMessage = "O campo Sexo deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "Sexo", Campo = "Sexo")]
        public virtual int SexoId { get; set; }

        [Display(Name = "Estado Civil")]
        //[Required(ErrorMessage = "O campo Estado Civil deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "EstadoCivil", Campo = "EstadoCivil")]
        public virtual int EstadoCivilId { get; set; }

        public virtual string EstadoCivilDescricao { get; set; }

        [Display(Name = "Raça")]
        [Required(ErrorMessage = "O campo Raça deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "Raca", Campo = "Raca")]
        public virtual int RacaId { get; set; }

        public virtual string RacaDescricao { get; set; }

        /*
        long CNPJ = 05662546000135;
        string CNPJFormatado = String.Format(@"{0:00\.000\.000\/0000\-00}", CNPJ); //Formatar de Long para CNPJ

        long RG = 305617853;
        string RGFormatado = String.Format(@"{0:00\.000\.000\-0}", RG);
        */
 

        [Display(Name = "Número do NIS")]
        [StringLength(11, MinimumLength = 11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O Número do NIS deverá estar no formato de 11 caracteres numéricos")]
        [ConverterEntidade]
        public virtual string NumeroNIS { get; set; }

        [Display(Name = "Código do INEP")]
        [StringLength(12, MinimumLength = 12)]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "O Número do NIS deverá estar no formato de 12 caracteres numéricos")]
        [ConverterEntidade]
        public virtual string CodigoINEP { get; set; }

        [Display(Name = "Nome da Mãe")]
        [Required(ErrorMessage = "O campo Nome da Mãe deve ser preenchido.")]
        [StringLength(64, MinimumLength = 4)]
        [ConverterEntidade]
        public virtual string NomeMae { get; set; }

        [Display(Name = "Nome do Pai")]
        [StringLength(64, MinimumLength = 4)]
        [ConverterEntidade]
        public virtual string NomePai { get; set; }

        [Display(Name = "Nome do Responsável Legal")]
        [StringLength(64, MinimumLength = 4)]
        [ConverterEntidade]
        public virtual string NomeResponsavel { get; set; }

        [Display(Name = "E-mail do Responsável")]
        [StringLength(64, MinimumLength = 4)]
        [DataType(DataType.EmailAddress)]
        [ConverterEntidade]
        public virtual string EmailResponsavel { get; set; }

        [Display(Name = "Nome do Cônjuge")]
        [StringLength(64, MinimumLength = 4)]
        [ConverterEntidade]
        public virtual string NomeConjuge { get; set; }

        [Display(Name = "Nacionalidade")]
        [Required(ErrorMessage = "O campo Nacionalidade deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "TipoNacionalidade")]
        public virtual int TipoNacionalidadeId { get; set; }

        public virtual string TipoNacionalidadeDescricao { get; set; }

        [Display(Name = "País de Origem")]
        [ConverterEntidade(NomeEntidade = "Pais", Campo = "PaisOrigem")]
        public virtual int PaisOrigemId { get; set; }

        [Display(Name = "País de Origem")]
        public virtual string PaisOrigemDescricao { get; set; }

        [Display(Name = "UF Nascimento")]
        [ConverterEntidade(NomeEntidade="Estado", Campo="UFNascimento")]
        public virtual int UFNascimentoId { get; set; }

        public virtual string UFNascimentoNome { get; set; }

        [Display(Name = "Cidade Nascimento")]
        [ConverterEntidade(NomeEntidade = "Municipio", Campo = "CidadeNascimento")]
        public virtual int CidadeNascimentoId { get; set; }

        public virtual int CidadeNascimentoNome { get; set; }

        [Display(Name = "Possui Deficiência?")]
        [Required(ErrorMessage = "O campo Possui Deficiência? deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagDeficiencia { get; set; }

        [Display(Name = "Tipo de Pessoa")]
        [Required(ErrorMessage = "O campo Tipo de Pessoa deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagTipoPessoa { get; set; }


        [Display(Name = "Escolarização Especial")]
        public virtual int EscolarizacaoEspecialId { get; set; }

        [Display(Name = "Turma Unificada")]
        public virtual int TurmaUnificadaId { get; set; }

        [Display(Name = "Transporte Público")]
        public virtual int TransportePublicoId { get; set; }

        //public virtual AlunoEndereco Endereco { get; set; }
        
        //public virtual AlunoDocumentacao Documentacao { get; set; }

        //public virtual AlunoInformacoes Informacoes { get; set; }
    }
}
