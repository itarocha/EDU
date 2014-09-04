using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class EscolaTurnoVO
    {
        public virtual int Id { get; set; }

        [Display(Name = "Escola")]
        [Required(ErrorMessage = "O campo Escola deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade = "Escola", Campo = "Escola")]
        public virtual int EscolaId { get; set; }

        [Display(Name = "Turno")]
        [ConverterEntidade(NomeEntidade = "Turno", Campo = "Turno")]
        public virtual int TurnoId { get; set; }

        [Display(Name = "Turno")]
        public virtual string TurnoDescricao { get; set; }

        [Display(Name = "Hora Inicial")]
        public virtual string HoraInicial { get; set; }

        [Display(Name = "Hora Final")]
        public virtual string HoraFinal { get; set; }
    }
}
