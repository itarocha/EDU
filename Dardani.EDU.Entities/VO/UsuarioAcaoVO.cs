using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class UsuarioAcaoVO
    {
        public virtual int Id { get; set; }

        public virtual int UsuarioId { get; set; }

        public virtual string[] ListaAcoes { get; set; }
    }
}
