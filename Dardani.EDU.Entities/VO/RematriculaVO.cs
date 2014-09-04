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
        public virtual int TurmaOrigemId { get; set; }

        public virtual int TurmaDestinoId { get; set; }

        public virtual int EscolarizacaoEspecialId { get; set; }
        
        public virtual int TurmaUnificadaId { get; set; }
        
        public virtual int TransportePublicoId { get; set; }

        public virtual int[] ListaAlunos { get; set; }
    }
}
