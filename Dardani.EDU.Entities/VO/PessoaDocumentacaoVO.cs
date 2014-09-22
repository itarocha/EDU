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
    public class PessoaDocumentacaoVO
    {
        public virtual int Id { get; set; }

        [ConverterEntidade(NomeEntidade = "Pessoa")]
        public virtual int PessoaId { get; set; }

        public virtual int TurmaId { get; set; }

        [Display(Name = "Situação do Documento na Escola")]
        [Required(ErrorMessage = "O campo Situação do Documento na Escola deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "SituacaoDocumento", Campo = "SituacaoDocumento")]
        public virtual int SituacaoDocumentoId { get; set; }

        public virtual string SituacaoDocumentoDescricao { get; set; }

        [Display(Name = "Modelo de Certidão")]
        [Required(ErrorMessage = "O campo Modelo de Certidão deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "ModeloCertidao")]
        public virtual int ModeloCertidaoId { get; set; }

        public virtual string ModeloCertidaoDescricao { get; set; }

        [Display(Name = "Tipo de Certidão")]
        [Required(ErrorMessage = "O campo Tipo de Certidão deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "TipoCertidao")]
        public virtual int TipoCertidaoId { get; set; }

        public virtual string TipoCertidaoDescricao { get; set; }

        [Display(Name = "Número da Certidão")]
        [ConverterEntidade]
        public virtual string CertidaoNumero { get; set; }

        [Display(Name = "Termo da Certidão")]
        [ConverterEntidade]
        public virtual string CertidaoTermo { get; set; }

        [Display(Name = "Certidão - Folha")]
        [ConverterEntidade]
        public virtual string CertidaoFolha { get; set; }

        [Display(Name = "Livro da Certidão")]
        [ConverterEntidade]
        public virtual string CertidaoLivro { get; set; }

        [Display(Name = "Cartório da Certidão")]
        [ConverterEntidade]
        public virtual string CertidaoNomeCartorio { get; set; }

        [Display(Name = "Data de Emissão da Certidão")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [ConverterEntidade]
        public virtual DateTime? CertidaoDataEmissao { get; set; }

        [Display(Name = "Cidade da Certidão")]
        [ConverterEntidade(NomeEntidade = "Municipio", Campo="CertidaoCidade")]
        public virtual int CertidaoCidadeId { get; set; }

        [Display(Name = "UF da Certidão")]
        [ConverterEntidade(NomeEntidade = "Estado", Campo = "CertidaoUF")]
        public virtual int CertidaoUFId { get; set; }

        [Display(Name = "Número Novo da Certidão")]
        [ConverterEntidade]
        public virtual string CertidaoNumeroNovo { get; set; }

        [Display(Name = "Número da RG")]
        [ConverterEntidade]
        public virtual string RGNumero { get; set; }

        [Display(Name = "Complemento da RG")]
        [ConverterEntidade]
        public virtual string RGComplemento { get; set; }

        [Display(Name = "Órgão da RG")]
        [ConverterEntidade]
        public virtual string RGOrgao { get; set; }

        [Display(Name = "Data de Emissão da RG")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [ConverterEntidade]
        public virtual DateTime? RGDataEmissao { get; set; }

        [Display(Name = "UF da RG")]
        [ConverterEntidade(NomeEntidade = "Estado", Campo = "RGUF")]
        public virtual int RGUFId { get; set; }

        [Display(Name = "Número do CPF")]
        //[StringLength(11, MinimumLength = 11)]
        //[RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deverá estar no formato 00000000000")]
        //[RegularExpression(@"^\d{3}.\d{3}.\d{3}-\d{2}$", ErrorMessage = "O CPF deverá estar no formato 000.000.000-00")]
        [ConverterEntidade]
        public virtual string CPFNumero { get; set; }

        [Display(Name = "Número do Documento Quando Estrangeiro")]
        [ConverterEntidade]
        public virtual string DocumentoEstrangeiroNumero { get; set; }

        [Display(Name = "Número da CNH")]
        [ConverterEntidade]
        public virtual string CNHNumero { get; set; }

        [Display(Name = "Categoria da CNH")]
        [ConverterEntidade]
        public virtual string CNHCategoria { get; set; }

        [Display(Name = "Data de Emissão da CNH")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [ConverterEntidade]
        public virtual DateTime? CNHDataEmissao { get; set; }

        [Display(Name = "Data de Validade da CNH")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [ConverterEntidade]
        public virtual DateTime? CNHDataValidade { get; set; }

        [Display(Name = "UF da CNH")]
        [ConverterEntidade(NomeEntidade = "Estado", Campo = "CNHUF")]
        public virtual int CNHUFId { get; set; }
    }
}
