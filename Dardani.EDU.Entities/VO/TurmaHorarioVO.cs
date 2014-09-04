using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class TurmaHorarioVO
    {
        public virtual int Id { get; set; }


        [Required(ErrorMessage = "Turma precisa ser informado.")]
        [Display(Name = "Turma")]
        [ConverterEntidade(NomeEntidade = "Turma", Campo = "Turma")]
        public virtual int TurmaId { get; set; }

        [Required(ErrorMessage = "Período precisa ser informada.")]
        [Display(Name = "Período")]
        [ConverterEntidade(NomeEntidade = "PeriodoAula", Campo = "PeriodoAula")]
        public virtual int PeriodoAulaId { get; set; }

        [Required(ErrorMessage = "Disciplina precisa ser informada.")]
        [Display(Name = "Disciplina")]
        [ConverterEntidade(NomeEntidade = "Disciplina", Campo = "Disciplina")]
        public virtual int DisciplinaId { get; set; }

        public virtual string DisciplinaDescricao { get; set; }

        [Required(ErrorMessage = "Profissional precisa ser preenchido.")]
        [Display(Name = "Profissional")]
        [ConverterEntidade(NomeEntidade = "Pessoa", Campo = "Pessoa")]
        public virtual int PessoaId { get; set; } // 200

        public virtual string PessoaNome { get; set; }

        [Required(ErrorMessage = "Dia da Semana precisa ser preenchido.")]
        [Display(Name = "Dia da Semana")]
        [ConverterEntidade]
        public virtual short FlagDiaSemana { get; set; } // 20 = 5 dias x 4 aulas dia
    }
}
