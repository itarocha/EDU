using System;
using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
using Petra.Util.Model;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dardani.EDU.Entities.VO;
using NHibernate.Transform;

namespace Dardani.EDU.BO.NH
{
    public class EscolaEducacionalDAO : GenericDAO<EscolaEducacional>
    {

        public EscolaEducacionalVO GetEscolaEducacionalVOById(int id)
        {
            EscolaEducacionalVO model =
                Session.CreateQuery("SELECT " +
                    "tb.Id as Id, "+
                    "tb.Id as EscolaId, "+
                    "tb.FlagEnsinoFundamentalCiclos as FlagEnsinoFundamentalCiclos, "+
                    "tb.FlagMaterialQuilombola as FlagMaterialQuilombola, "+
                    "tb.FlagMaterialIndigena as FlagMaterialIndigena, "+
                    "tb.FlagEducacaoIndigena as FlagEducacaoIndigena, "+
                    "tb.FlagEnsinoLinguaPortuguesa as FlagEnsinoLinguaPortuguesa, "+
                    "tb.FlagEnsinoLinguaIndigena as FlagEnsinoLinguaIndigena, "+
                    "tb.FlagBrasilAlfabetizado as FlagBrasilAlfabetizado, "+
                    "tb.FlagFinalSemana as FlagFinalSemana, "+
                    "tb.FlagAlternancia as FlagAlternancia "+
                    "FROM EscolaEducacional tb " +
                    "LEFT JOIN tb.AEE aee " +
                    "LEFT JOIN tb.AtividadeComplementar ac " +
                    "LEFT JOIN tb.LocalizacaoDiferenciada ld " +
                    "LEFT JOIN tb.LinguaIndigena li " +
                    "WHERE tb.Id = :id "
                )
                .SetParameter("id", id)
                .SetResultTransformer(Transformers.AliasToBean(typeof(EscolaEducacionalVO)))
                .UniqueResult<EscolaEducacionalVO>();

            return model;


            /*
            EscolaEducacionalVO avo = null;
            EscolaEducacional a = null;
            AEE aee = null;
            AtividadeComplementar ac = null;
            LocalizacaoDiferenciada ld = null;
            LinguaIndigena li = null;

            EscolaEducacionalVO model =
            Session.QueryOver<EscolaEducacional>(() => a)
                .Left.JoinQueryOver<AEE>(() => a.AEE, () => aee)
                .Left.JoinQueryOver<AtividadeComplementar>(() => a.AtividadeComplementar, () => ac)
                .Left.JoinQueryOver<LocalizacaoDiferenciada>(() => a.LocalizacaoDiferenciada, () => ld)
                .Left.JoinQueryOver<LinguaIndigena>(() => a.LinguaIndigena, () => li)
                .SelectList(list => list
                    .Select(() => a.Id).WithAlias(() => avo.Id)
                    .Select(() => a.Id).WithAlias(() => avo.EscolaId)
                    .Select(() => a.FlagEnsinoFundamentalCiclos).WithAlias(() => avo.FlagEnsinoFundamentalCiclos)
                    .Select(() => a.FlagMaterialQuilombola).WithAlias(() => avo.FlagMaterialQuilombola)
                    .Select(() => a.FlagMaterialIndigena).WithAlias(() => avo.FlagMaterialIndigena)
                    .Select(() => a.FlagEducacaoIndigena).WithAlias(() => avo.FlagEducacaoIndigena)
                    .Select(() => a.FlagEnsinoLinguaPortuguesa).WithAlias(() => avo.FlagEnsinoLinguaPortuguesa)
                    .Select(() => a.FlagEnsinoLinguaIndigena).WithAlias(() => avo.FlagEnsinoLinguaIndigena)
                    .Select(() => a.FlagBrasilAlfabetizado).WithAlias(() => avo.FlagBrasilAlfabetizado)
                    .Select(() => a.FlagFinalSemana).WithAlias(() => avo.FlagFinalSemana)
                    .Select(() => a.FlagAlternancia).WithAlias(() => avo.FlagAlternancia)
                ).Where(() => a.Id == id)
                .TransformUsing(Transformers.AliasToBean<EscolaEducacionalVO>())
                .SingleOrDefault<EscolaEducacionalVO>();

            return model;
            */
        }

    } // END CLASS
} // END NAMESPACE
