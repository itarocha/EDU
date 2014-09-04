using Dardani.EDU.Entities.Model;
using NHibernate.Event;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class Pessoa : ISaveOrUpdateEventListener
    {
        public virtual int Id { get; set; } 

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo Nome deve ser preenchido.")]
        [StringLength(64, MinimumLength = 4)]
        public virtual string Nome { get; set; } 

        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "O campo Data de Nascimento deve ser preenchido.")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public virtual DateTime DataNascimento { get; set; }

        [Display(Name = "Sexo")]
        [Required(ErrorMessage = "O campo Sexo deve ser preenchido.")]
        public virtual Sexo Sexo { get; set; }

        //public virtual int SexoId { get; set; }

        [Display(Name = "Raça")]
        [Required(ErrorMessage = "O campo Raça deve ser preenchido.")]
        public virtual Raca Raca { get; set; }

        //public virtual int RacaId { get; set; }
        
        [Display(Name = "Número do NIS")]
        [StringLength(11, MinimumLength = 11)]
        public virtual string NumeroNIS { get; set; } 

        [Display(Name = "Código do INEP")]
        public virtual string CodigoINEP { get; set; } 

        [Display(Name = "Nome da Mãe")]
        [Required(ErrorMessage = "O campo Nome da Mãe deve ser preenchido.")]
        [StringLength(64, MinimumLength = 4)]
        public virtual string NomeMae { get; set; } 

        [Display(Name = "Nome do Pai")]
        [StringLength(64, MinimumLength = 4)]
        public virtual string NomePai { get; set; } 

        [Display(Name = "Nome do Responsável Legal")]
        [StringLength(64, MinimumLength = 4)]
        public virtual string NomeResponsavel { get; set; } 

        [Display(Name = "E-mail do Responsável")]
        [StringLength(64, MinimumLength = 4)]
        public virtual string EmailResponsavel { get; set; }

        [Display(Name = "Estado Civil")]
        //[Required(ErrorMessage = "O campo Estado Civil deve ser preenchido.")]
        public virtual EstadoCivil EstadoCivil { get; set; }

        //public virtual int EstadoCivilId { get; set; }

        [Display(Name = "Nome do Cônjuge")]
        [StringLength(64, MinimumLength = 4)]
        public virtual string NomeConjuge { get; set; }

        [Display(Name = "Nacionalidade")]
        [Required(ErrorMessage = "O campo Nacionalidade deve ser preenchido.")]
        public virtual TipoNacionalidade TipoNacionalidade { get; set; }

        //public virtual int TipoNacionalidadeId { get; set; }

        [Display(Name = "Pais")]
        public virtual Pais PaisOrigem { get; set; }

        //public virtual int PaisOrigemId { get; set; }

        [Display(Name = "UF Nascimento")]
        public virtual Estado UFNascimento { get; set; } 

        [Display(Name = "Cidade Nascimento")]
        [StringLength(64, MinimumLength = 3)]
        public virtual Municipio CidadeNascimento { get; set; } 

        [Display(Name = "Possui Deficiência?")]
        [Required(ErrorMessage = "O campo Possui Deficiência? deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        public virtual string FlagDeficiencia { get; set; } 

        [Display(Name = "Tipo de Pessoa")]
        [Required(ErrorMessage = "O campo Tipo de Pessoa deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        public virtual string FlagTipoPessoa { get; set; }

        
        public virtual PessoaEndereco Endereco { get; set; } 

        public virtual PessoaDocumentacao Documentacao { get; set; } 
        

        //public virtual AlunoDetalhes AlunoDetalhes { get; set; } 


        public virtual void OnSaveOrUpdate(SaveOrUpdateEvent @event)
        {
            Object origem = @event.Entity;

            Type t = origem.GetType();
            foreach (PropertyInfo piOrigem in t.GetProperties())
            {
                if (piOrigem.GetType().Equals(typeof(String)))
                {

                    Type tipo = piOrigem.GetType();

                    if (piOrigem.GetValue(origem) != null)
                    {
                        piOrigem.SetValue(origem, piOrigem.GetValue(origem).ToString().ToUpperInvariant());
                    }

                }
            }

        }
    }
}
