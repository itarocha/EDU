using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class EscolaEducacionalMap : ClassMap<EscolaEducacional>
    {
        public EscolaEducacionalMap()
        {
            Table("EDU_ESCOLA_EDUCACIONAL");
            Id(x => x.Id).GeneratedBy.Foreign("Escola");
            HasOne(x => x.Escola).Constrained();

            References(x => x.AEE).Column("ID_AEE").Not.Nullable();
            References(x => x.AtividadeComplementar).Column("ID_ATIVIDADE_COMPLEMENTAR").Not.Nullable();
            References(x => x.LocalizacaoDiferenciada).Column("ID_LOCALIZACAO_DIFERENCIADA").Not.Nullable();
            References(x => x.LinguaIndigena).Column("ID_LINGUA_INDIGENA").Not.Nullable();

            Map(x => x.FlagEnsinoFundamentalCiclos).Column("FL_ENSINO_FUNDAMENTAL_CICLOS").Length(1).Not.Nullable();
            Map(x => x.FlagMaterialQuilombola).Column("FL_MATERIAL_QUILOMBOLA").Length(1).Not.Nullable();
            Map(x => x.FlagMaterialIndigena).Column("FL_MATERIAL_INDIGENA").Length(1).Not.Nullable();
            Map(x => x.FlagEducacaoIndigena).Column("FL_EDUCACAO_INDIGENA").Length(1).Not.Nullable();
            Map(x => x.FlagEnsinoLinguaIndigena).Column("FL_ENSINO_LINGUA_INDIGENA").Length(1).Not.Nullable();
            Map(x => x.FlagEnsinoLinguaPortuguesa).Column("FL_ENSINO_LINGUA_PORTUGUESA").Length(1).Not.Nullable();
            Map(x => x.FlagBrasilAlfabetizado).Column("FL_BRASIL_ALFABETIZADO").Length(1).Not.Nullable();
            Map(x => x.FlagFinalSemana).Column("FL_FINAL_SEMANA").Length(1).Not.Nullable();
            Map(x => x.FlagAlternancia).Column("FL_ALTERNANCIA").Length(1).Not.Nullable();
        }
    }
}
