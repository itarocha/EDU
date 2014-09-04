using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Table("GER_USUARIO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_USUARIO");
            Map(x => x.Nome).Column("DS_NOME").Length(64).Not.Nullable();
            Map(x => x.DataNascimento).Column("DT_NASCIMENTO").Not.Nullable();
            Map(x => x.NumeroCPF).Column("VL_CPF").Not.Nullable();
            Map(x => x.Nivel).Column("DS_NIVEL").Length(32).Not.Nullable();
            Map(x => x.Ativo).Column("FL_ATIVO").Length(1).Not.Nullable();
            References(x => x.Sexo).Column("ID_SEXO").Not.Nullable();
            //References(x => x.Acesso).Column("ID_ACESSO").Not.Nullable();
        }
    }
}


