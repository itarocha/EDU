using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class MatrizDisciplinaVO
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Matriz precisa ser informada.")]
        [Display(Name = "Matriz")]
        [ConverterEntidade(NomeEntidade = "Matriz", Campo = "Matriz")]
        public virtual int MatrizId { get; set; }

        [Required(ErrorMessage = "Disciplina precisa ser informada.")]
        [Display(Name = "Disciplina")]
        [ConverterEntidade(NomeEntidade = "Disciplina", Campo = "Disciplina")]
        public virtual int DisciplinaId { get; set; }

        public virtual string DisciplinaDescricao { get; set; }

        public virtual string DisciplinaDescricaoAbreviada { get; set; }

        [Display(Name = "Conceito")]
        [ConverterEntidade(NomeEntidade = "Conceito", Campo = "Conceito")]
        public virtual int ConceitoId { get; set; }

        public virtual string ConceitoDescricao { get; set; }

        [Required(ErrorMessage = "Carga Horária Semanal precisa ser preenchido.")]
        [Display(Name = "Carga Horária Semanal")]
        [ConverterEntidade]
        public virtual short CargaHorariaSemanal { get; set; } // 2

        [Display(Name = "Tipo de Avaliação")]
        [Required(ErrorMessage = "O campo Tipo de Avaliação deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagTipoAvaliacao { get; set; }  // N = Nota; C = Conceito; P = Parecer

        [Display(Name = "Tipo de Avaliação")]
        public virtual string FlagTipoAvaliacaoDescricao
        {
            get
            {
                if (this.FlagTipoAvaliacao == "N")
                {
                    return "Nota";
                }
                else if (this.FlagTipoAvaliacao == "C")
                {
                    return "Conceito";
                } 
                else if (this.FlagTipoAvaliacao == "P")
                {
                    return "Parecer";
                }
                else return "";
            }
        }
        
        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "O campo Categoria deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagCategoria { get; set; }  // N = Base Nacional Comum; P = Parte Diversificada
	
        [Display(Name = "Aceita Dispensa")]
        [Required(ErrorMessage = "O campo Aceita Dispensa deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagAceitaDispensa { get; set; }  // S = Sim; N = Não

        [Display(Name = "Reprova")]
        [Required(ErrorMessage = "O campo Reprova deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagReprova { get; set; }  // S = Sim; N = Não
    }
}
