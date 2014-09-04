using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class BaseCurricularVO
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Descrição precisa ser preenchida.")]
        [StringLength(64, MinimumLength = 3)]
        [Display(Name = "Descrição")]
        [ConverterEntidade]
        public virtual string Descricao { get; set; }

        [Required(ErrorMessage = "Descrição Turno precisa ser preenchida.")]
        [StringLength(32, MinimumLength = 2)]
        [Display(Name = "Descrição Turno")]
        [ConverterEntidade]
        public virtual string DescricaoTurno { get; set; }

        [Required(ErrorMessage = "Frequência precisa ser preenchida.")]
        [StringLength(1)]
        [Display(Name = "Frequência")] // P]
        [ConverterEntidade]
        public virtual string FlagMediaFrequencia { get; set; }

        [Required(ErrorMessage = "Controle de Frequência precisa ser preenchida.")]
        [StringLength(1)]
        [Display(Name = "Controle de Frequência")] // G/I
        [ConverterEntidade]
        public virtual string FlagControleFrequencia { get; set; }

        [Required(ErrorMessage = "Base Conclui Curso precisa ser preenchida.")]
        [StringLength(1)]
        [Display(Name = "Base Conclui Curso")] // S/N
        [ConverterEntidade]
        public virtual string FlagConclusao { get; set; }

        [Display(Name = "Curso")]
        [Required(ErrorMessage = "Curso precisa ser informado")]
        public virtual int CursoId { get; set; }

        public virtual string CursoDescricao { get; set; }


        [Display(Name = "Regime de Matrícula")]
        [Required(ErrorMessage = "Regime de Matrícula precisa ser informado")]
        public virtual int RegimeMatriculaId { get; set; }

        public virtual string RegimeMatriculaDescricao { get; set; }
        
        public virtual string RegimeMatriculaDescricaoAbreviada { get; set; }
    }
}
