using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class UsuarioAcaoMap : ClassMap<UsuarioAcao>
    {
        public UsuarioAcaoMap()
        {
            Table("GER_USUARIO_ACAO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_USUARIO_ACAO");
            References(x => x.Usuario).Column("ID_USUARIO").Not.Nullable();
            References(x => x.Acao).Column("ID_ACAO").Not.Nullable();
        }
    }
}
