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
    public class MatriculaVO
    {
        public virtual int Id { get; set; }

        [Display(Name = "Aluno")]
        public virtual Pessoa Pessoa { get; set; }

        [ConverterEntidade(NomeEntidade = "Pessoa")]
        public virtual int PessoaId { get; set; }
        
        public virtual string PessoaNome { get; set; }
        
        public virtual DateTime PessoaDataNascimento { get; set; }
        
        public virtual string PessoaNumeroNIS { get; set; }

        [Display(Name = "Ano Letivo")]
        [Required(ErrorMessage = "O campo Ano Letivo deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade="AnoLetivo")]
        public virtual int AnoLetivoId { get; set; }

        public virtual int AnoLetivoAno { get; set; }

        [Display(Name = "Turma")]
        [Required(ErrorMessage = "O campo Turma deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "Turma")]
        public virtual int TurmaId { get; set; }

        public virtual string TurmaNome { get; set; }

        [Display(Name = "Escolarização Especial")]
        [Required(ErrorMessage = "O campo Escolarização Especial deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "EscolarizacaoEspecial")]
        public virtual int EscolarizacaoEspecialId { get; set; }

        public virtual string EscolarizacaoEspecialDescricao { get; set; }

        [Display(Name = "Transporte Público")]
        [Required(ErrorMessage = "O campo Transporte Público deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "TransportePublico")]
        public virtual int TransportePublicoId { get; set; }

        public virtual string TransportePublicoDescricao { get; set; }

        [Display(Name = "Turma Unificada")]
        [Required(ErrorMessage = "O campo Turma Unificada deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "TurmaUnificada")]
        public virtual int TurmaUnificadaId { get; set; }

        public virtual string TurmaUnificadaDescricao { get; set; }

        [Display(Name = "Rematricular")]
        [Required(ErrorMessage = "O campo Rematricular Letivo deve ser preenchido.")]
        [ConverterEntidade]
        public virtual string FlagRematricular { get; set; }

        public MatriculaVO() {
            Pessoa = new Pessoa();
        }
    }
}
