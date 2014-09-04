using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class PessoaDocumentacao
    {
        public virtual int Id { get; set; } 

        public virtual Pessoa Pessoa { get; set; } 

        [Display(Name = "Situação do Documento na Escola")]
        [Required(ErrorMessage = "O campo Situação do Documento na Escola deve ser preenchido.")]
        public virtual SituacaoDocumento SituacaoDocumento { get; set; } 

        [Display(Name = "Modelo de Certidão")]
        [Required(ErrorMessage = "O campo Modelo de Certidão deve ser preenchido.")]
        public virtual ModeloCertidao ModeloCertidao { get; set; } 

        [Display(Name = "Tipo de Certidão")]
        [Required(ErrorMessage = "O campo Tipo de Certidão deve ser preenchido.")]
        public virtual TipoCertidao TipoCertidao { get; set; } 

        [Display(Name = "Número da Certidão")]
        public virtual string CertidaoNumero { get; set; } 

        [Display(Name = "Termo da Certidão")]
        public virtual string CertidaoTermo { get; set; } 

        [Display(Name = "Certidão - Folha")]
        public virtual string CertidaoFolha { get; set; } 

        [Display(Name = "Livro da Certidão")]
        public virtual string CertidaoLivro { get; set; } 

        [Display(Name = "Cartório da Certidão")]
        public virtual string CertidaoNomeCartorio { get; set; } 

        [Display(Name = "Data de Emissão da Certidão")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public virtual DateTime? CertidaoDataEmissao { get; set; } 

        [Display(Name = "Cidade da Certidão")]
        public virtual Municipio CertidaoCidade { get; set; } 

        [Display(Name = "UF da Certidão")]
        public virtual Estado CertidaoUF { get; set; } 

        [Display(Name = "Cartório da Certidão")]
        public virtual string CertidaoCartorio { get; set; } 

        [Display(Name = "Número Novo da Certidão")]
        public virtual string CertidaoNumeroNovo { get; set; } 

        [Display(Name = "Número da RG")]
        public virtual string RGNumero { get; set; } 

        [Display(Name = "Complemento da RG")]
        public virtual string RGComplemento { get; set; } 

        [Display(Name = "Órgão da RG")]
        public virtual string RGOrgao { get; set; } 

        [Display(Name = "Data de Emissão da RG")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public virtual DateTime ? RGDataEmissao { get; set; } 

        [Display(Name = "UF da RG")]
        public virtual Estado RGUF { get; set; } 

        [Display(Name = "Número do CPF")]
        [StringLength(11, MinimumLength = 11)]
        public virtual string CPFNumero { get; set; } 

        [Display(Name = "Número do Documento Quando Estrangeiro")]
        public virtual string DocumentoEstrangeiroNumero { get; set; } 

        [Display(Name = "Número da CNH")]
        public virtual string CNHNumero { get; set; } 

        [Display(Name = "Categoria da CNH")]
        public virtual string CNHCategoria { get; set; } 

        [Display(Name = "Data de Emissão da CNH")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public virtual DateTime ? CNHDataEmissao { get; set; } 

        [Display(Name = "Data de Validade da CNH")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public virtual DateTime ? CNHDataValidade { get; set; } 

        [Display(Name = "UF da CNH")]
        public virtual Estado CNHUF { get; set; } 

    }
}
