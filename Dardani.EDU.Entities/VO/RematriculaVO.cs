using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class RematriculaVO
    {
        [Display(Name = "Turma de Origem")]
        public virtual int TurmaOrigemId { get; set; }

        [Display(Name = "Turma de Destino")]
        public virtual int TurmaDestinoId { get; set; }

        [Display(Name = "Escolarização Especial")]
        public virtual int EscolarizacaoEspecialId { get; set; }

        [Display(Name = "Turma Unificada")]
        public virtual int TurmaUnificadaId { get; set; }

        [Display(Name = "Transporte Público")]
        public virtual int TransportePublicoId { get; set; }

        public virtual int[] ListaAlunos { get; set; }
    }
}
