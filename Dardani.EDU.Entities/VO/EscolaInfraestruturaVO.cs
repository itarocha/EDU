using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class EscolaInfraestruturaVO
    {
        public virtual int Id { get; set; }

        public virtual int EscolaId { get; set; }

        public virtual int[] ListaItensInfraestrutura { get; set; }
    }
}
