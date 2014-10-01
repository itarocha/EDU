using System;
using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
using Dardani.EDU.Entities.VO;
//using Petra.Util.Model;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Transform;

namespace Dardani.EDU.BO.NH
{
    public class ConceitoNivelDAO : GenericDAO<ConceitoNivel>
    {

        public IEnumerable<ConceitoNivel> GetListagem()
        {
            IEnumerable<ConceitoNivel> lista = Session.QueryOver<ConceitoNivel>()
                .OrderBy(x => x.Descricao).Desc.List();
            
            return lista;
        }

        public ConceitoNivelVO GetVOById(int id)
        {
            
            ConceitoNivelVO model = Session.CreateQuery(
                "SELECT " +
                "tb.Id as Id, " +
                //"e.Id as EscolaId, "+
                "tb.Descricao as Descricao, " +
                "tb.Peso as Peso, " +
                "c.Id as ConceitoId, " +
                "c.Descricao as ConceitoDescricao " +
                "FROM ConceitoNivel tb " +
                "INNER JOIN tb.Conceito c "+
                "WHERE tb.Id = :id")
                .SetParameter("id", id)
                .SetResultTransformer(Transformers.AliasToBean(typeof(ConceitoNivelVO)))
                .UniqueResult<ConceitoNivelVO>();

            return model;
        }

        public IEnumerable<ConceitoNivelVO> GetByConceitoId(int conceitoId)
        {
            IEnumerable<ConceitoNivelVO> model = Session.CreateQuery(
                "SELECT " +
                "tb.Id as Id, " +
                //"e.Id as EscolaId, "+
                "tb.Descricao as Descricao, " +
                "tb.Peso as Peso, " +
                "c.Id as ConceitoId, " +
                "c.Descricao as ConceitoDescricao " +
                "FROM ConceitoNivel tb " +
                "INNER JOIN tb.Conceito c "+
                "WHERE c.Id = :conceitoId")
                .SetParameter("conceitoId", conceitoId)
                .SetResultTransformer(Transformers.AliasToBean(typeof(ConceitoNivelVO)))
                .List<ConceitoNivelVO>();

            return model;
        }

        

    } // END CLASS
} // END NAMESPACE
