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
    public class PessoaDisciplinaDAO : GenericDAO<PessoaDisciplina>
    {
        public IEnumerable<PessoaVO> GetListaPessoasByDisciplina(int disciplinaId)
        {
            IEnumerable<PessoaVO> model =
            Session.CreateQuery(
            "SELECT " +
            "p.Id as Id, " +
            "p.Nome as Nome, " +
            "p.NumeroNIS as NumeroNIS, " +
            "p.DataNascimento as DataNascimento " +
            "FROM PessoaDisciplina pd " +
            "INNER JOIN pd.Pessoa p  " +
            "INNER JOIN pd.Disciplina d  " +
            "WHERE d.Id = :disciplinaId " +
            "ORDER BY p.Nome ")
            .SetParameter("disciplinaId", disciplinaId)
            .SetResultTransformer(Transformers.AliasToBean(typeof(PessoaVO)))
            .List<PessoaVO>();

            return model;
        }


    } // END CLASS
} // END NAMESPACE
