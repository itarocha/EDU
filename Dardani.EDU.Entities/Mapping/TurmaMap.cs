using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class TurmaMap : ClassMap<Turma>
    {
        public TurmaMap()
        {
            Table("EDU_TURMA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_TURMA");
            Map(x => x.Nome).Column("DS_NOME").Length(128).Not.Nullable();
            Map(x => x.CodigoINEP).Column("DS_CODIGO_INEP").Length(32);
            References(x => x.Escola).Column("ID_ESCOLA").Not.Nullable();
            References(x => x.Calendario).Column("ID_CALENDARIO").Not.Nullable();
            References(x => x.Modalidade).Column("ID_MODALIDADE");//.Not.Nullable();
            References(x => x.Etapa).Column("ID_ETAPA"); //.Not.Nullable();
            References(x => x.Turno).Column("ID_TURNO").Not.Nullable();
            References(x => x.Horario).Column("ID_HORARIO").Not.Nullable();
            References(x => x.Sala).Column("ID_SALA");
            References(x => x.TipoAtendimento).Column("ID_TIPO_ATENDIMENTO").Not.Nullable();
            Map(x => x.FlagDomingo).Column("FL_DOM").Length(1).Not.Nullable();
            Map(x => x.FlagSegunda).Column("FL_SEG").Length(1).Not.Nullable();
            Map(x => x.FlagTerca).Column("FL_TER").Length(1).Not.Nullable();
            Map(x => x.FlagQuarta).Column("FL_QUA").Length(1).Not.Nullable();
            Map(x => x.FlagQuinta).Column("FL_QUI").Length(1).Not.Nullable();
            Map(x => x.FlagSexta).Column("FL_SEX").Length(1).Not.Nullable();
            Map(x => x.FlagSabado).Column("FL_SAB").Length(1).Not.Nullable();
            Map(x => x.FlagPrograma).Column("FL_PROGRAMA").Length(1).Not.Nullable();
        }
    }
}
