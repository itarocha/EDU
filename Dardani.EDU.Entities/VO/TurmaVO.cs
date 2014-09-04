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
    public class TurmaVO
    {
        public virtual int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(128, MinimumLength = 4)]
        [ConverterEntidade]
        public virtual string Nome { get; set; }

        [Display(Name = "Código do INEP")]
        [ConverterEntidade]
        public virtual string CodigoINEP { get; set; }

        [Display(Name = "Escola")]
        [Required(ErrorMessage = "O campo Escola deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade="Escola")]
        public virtual int EscolaId { get; set; }

        public virtual string EscolaNome { get; set; }

        [Display(Name = "Calendario")]
        [Required(ErrorMessage = "O campo Calendario deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "Calendario")]
        public virtual int CalendarioId { get; set; }

        public virtual string CalendarioDescricao { get; set; }

        public virtual int CalendarioAno { get; set; }

        [Display(Name = "Modalidade")]
        [Required(ErrorMessage = "O campo Modalidade deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "Modalidade")]
        public virtual int ModalidadeId { get; set; }

        public virtual string ModalidadeDescricao { get; set; }

        [Display(Name = "Etapa")]
        [Required(ErrorMessage = "O campo Etapa deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "Etapa")]
        public virtual int EtapaId { get; set; }

        public virtual string EtapaDescricao { get; set; }

        public virtual string TipoEnsinoDescricao { get; set; }

        public virtual string SerieDescricao { get; set; }

        [Display(Name = "Turno")]
        [Required(ErrorMessage = "O campo Turno deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "Turno")]
        public virtual int TurnoId { get; set; }

        public virtual string TurnoDescricao { get; set; }

        [Display(Name = "Horário")]
        [Required(ErrorMessage = "O campo Horário deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "Horario")]
        public virtual int HorarioId { get; set; }

        public virtual string HorarioDescricao { get; set; }

        [Display(Name = "Hora Inicial")]
        [StringLength(8, MinimumLength = 4)]
        public virtual string HorarioHoraInicial { get; set; }

        [Display(Name = "Hora Final")]
        [StringLength(8, MinimumLength = 4)]
        public virtual string HorarioHoraFinal { get; set; }

        [Display(Name = "Sala")]
        [ConverterEntidade(NomeEntidade = "Sala",Campo = "Sala")]
        public virtual int SalaId { get; set; }

        public virtual string SalaDescricao { get; set; }

        [Display(Name = "TipoAtendimento")]
        [Required(ErrorMessage = "O campo TipoAtendimento deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "TipoAtendimento")]
        public virtual int TipoAtendimentoId { get; set; }

        public virtual string TipoAtendimentoDescricao { get; set; }

        [Display(Name = "Domingo")]
        [Required(ErrorMessage = "O campo Domingo deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagDomingo { get; set; }

        [Display(Name = "Segunda")]
        [Required(ErrorMessage = "O campo Segunda deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagSegunda { get; set; }

        [Display(Name = "Terça")]
        [Required(ErrorMessage = "O campo Terça deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagTerca { get; set; }

        [Display(Name = "Quarta")]
        [Required(ErrorMessage = "O campo Quarta deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagQuarta { get; set; }

        [Display(Name = "Quinta")]
        [Required(ErrorMessage = "O campo Quinta deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagQuinta { get; set; }

        [Display(Name = "Sexta")]
        [Required(ErrorMessage = "O campo Sexta deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagSexta { get; set; }

        [Display(Name = "Sábado")]
        [Required(ErrorMessage = "O campo Sábado deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagSabado { get; set; }

        [Display(Name = "Programa Mais Educação/Ensino Médio Inovador")]
        [Required(ErrorMessage = "O campo Programa Mais Educação/Ensino Médio Inovador deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagPrograma { get; set; }

        public virtual string CkbDom { get; set; }
        public virtual string CkbSeg { get; set; }
        public virtual string CkbTer { get; set; }
        public virtual string CkbQua { get; set; }
        public virtual string CkbQui { get; set; }
        public virtual string CkbSex { get; set; }
        public virtual string CkbSab { get; set; }

        public TurmaVO() {
            FlagDomingo = "N";
            FlagSegunda = "S";
            FlagTerca = "S";
            FlagQuarta = "S";
            FlagQuinta = "S";
            FlagSexta = "S";
            FlagSabado = "N";
            FlagPrograma = "N";
        }

    }
}
