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
using Petra.DAO.Util;

namespace Dardani.EDU.BO.NH
{
    public class PessoaDisciplinaDAO : GenericDAO<PessoaDisciplina>
    {
        public IEnumerable<PessoaVO> GetListaPessoasByDisciplina(int disciplinaId)
        {

            
            //NHibernateBase.Session.Dispose();
            //ISession s = NHibernateBase.Session;
            //Session.Dispose();

            //NHibernateBase.CloseSession();

            //ISession s = NHibernateBase.Session;


            //s.Reconnect();
            //s.Clear();

            //s.Clear();
            //Session.CacheMode(
            //Session.Flush();

            //NHibernateBase.CloseSession();
            //NHibernateBase.OpenSession();

            //StatelessSession.Close();
            //StatelessSession.Connection.Open();
            //StatelessSession.Refresh();

            //Session.BeginTransaction();

            //Session.Evict(
            
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
            .SetCacheMode(CacheMode.Refresh)
            .SetParameter("disciplinaId", disciplinaId)
            .SetCacheMode(CacheMode.Refresh)
            //.CacheMode(CacheMode.Refresh)
            .SetResultTransformer(Transformers.AliasToBean(typeof(PessoaVO)))
            .List<PessoaVO>();

            //Session.Transaction.Commit();

            return model;
        }

        /*
        public IEnumerable<PessoaVO> GetListaPessoasByDisciplina(int disciplinaId)
        {

            PessoaVO pd = new PessoaVO();
            //pd.Id = pessoaId;
            //pd.PessoaId = pessoaId;

            IEnumerable<PessoaDisciplina> itens =
                Session.QueryOver<PessoaDisciplina>().Where(x => x.Disciplina.Id == disciplinaId).List();
            List<PessoaVO> list = new List<PessoaVO>();
            foreach (PessoaDisciplina i in itens)
            {
                list.Add(new PessoaVO() { Id = i.Pessoa.Id, Nome = i.Pessoa.Nome });
            }
            //pd.ListaDisciplinas = list.ToArray();

            return list;
        }
        */

    } // END CLASS
} // END NAMESPACE
