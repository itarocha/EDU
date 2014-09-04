using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class Turma
    {
        public virtual int Id { get; set; } 

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo Nome deve ser preenchido.")]
        [StringLength(128, MinimumLength = 4)]
        public virtual string Nome { get; set; }

        [Display(Name = "Código do INEP")]
        public virtual string CodigoINEP { get; set; } 

        [Display(Name = "Escola")]
        [Required(ErrorMessage = "O campo Escola deve ser preenchido.")]
        public virtual Escola Escola { get; set; } 

        [Display(Name = "Calendario")]
        [Required(ErrorMessage = "O campo Calendario deve ser preenchido.")]
        public virtual Calendario Calendario { get; set; } 

        [Display(Name = "Modalidade")]
        [Required(ErrorMessage = "O campo Modalidade deve ser preenchido.")]
        public virtual Modalidade Modalidade { get; set; } 

        [Display(Name = "Etapa")]
        [Required(ErrorMessage = "O campo Etapa deve ser preenchido.")]
        public virtual Etapa Etapa { get; set; }

        [Display(Name = "Turno")]
        [Required(ErrorMessage = "O campo Turno deve ser preenchido.")]
        public virtual Turno Turno { get; set; }

        [Display(Name = "Horário")]
        [Required(ErrorMessage = "O campo Horário deve ser preenchido.")]
        public virtual Horario Horario { get; set; }

        [Display(Name = "Sala")]
        public virtual Sala Sala { get; set; } 

        [Display(Name = "TipoAtendimento")]
        [Required(ErrorMessage = "O campo TipoAtendimento deve ser preenchido.")]
        public virtual TipoAtendimento TipoAtendimento { get; set; } 

        [Display(Name = "Domingo")]
        [Required(ErrorMessage = "O campo Domingo deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        public virtual string FlagDomingo { get; set; } 

        [Display(Name = "Segunda")]
        [Required(ErrorMessage = "O campo Segunda deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        public virtual string FlagSegunda { get; set; } 

        [Display(Name = "Terça")]
        [Required(ErrorMessage = "O campo Terça deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        public virtual string FlagTerca { get; set; } 

        [Display(Name = "Quarta")]
        [Required(ErrorMessage = "O campo Quarta deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        public virtual string FlagQuarta { get; set; } 

        [Display(Name = "Quinta")]
        [Required(ErrorMessage = "O campo Quinta deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        public virtual string FlagQuinta { get; set; } 

        [Display(Name = "Sexta")]
        [Required(ErrorMessage = "O campo Sexta deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        public virtual string FlagSexta { get; set; } 

        [Display(Name = "Sábado")]
        [Required(ErrorMessage = "O campo Sábado deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        public virtual string FlagSabado { get; set; } 

        [Display(Name = "Programa Mais Educação/Ensino Médio Inovador")]
        [Required(ErrorMessage = "O campo Programa Mais Educação/Ensino Médio Inovador deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        public virtual string FlagPrograma { get; set; } 

    }
}
