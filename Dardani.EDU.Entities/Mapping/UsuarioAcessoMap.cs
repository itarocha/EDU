using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class UsuarioAcessoMap : ClassMap<UsuarioAcesso>
    {
        public UsuarioAcessoMap()
        {
            Table("GER_USUARIO_ACESSO");
            Id(x => x.Id).GeneratedBy.Foreign("Usuario");
            HasOne(x => x.Usuario).Constrained();
            Map(x => x.NomeUsuario).Column("DS_NOME_USUARIO").Length(32).Not.Nullable();
            Map(x => x.SenhaAcesso).Column("DS_SENHA_ACESSO").Length(32).Not.Nullable();
            Map(x => x.Email).Column("DS_EMAIL").Length(64).Not.Nullable();
        }
    }
}
