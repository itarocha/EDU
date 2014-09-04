using System;
using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
using Petra.Util.Model;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Transform;
using Dardani.EDU.Entities.VO;
using NHibernate.Criterion;

namespace Dardani.EDU.BO.NH
{
    public class PessoaDAO : GenericDAO<Pessoa>
    {
        public Pessoa GetByCodigoINEP(string codigo)
        {
            Pessoa value = Session.QueryOver<Pessoa>()
                .Where(x => x.CodigoINEP == codigo)
                .List().FirstOrDefault();
            return value;
        }

        public PessoaDisciplinaVO GetPessoaDisciplina(int pessoaId)
        {

            PessoaDisciplinaVO pd = new PessoaDisciplinaVO();
            pd.Id = pessoaId;
            pd.PessoaId = pessoaId;

            IEnumerable<PessoaDisciplina> itens =
                Session.QueryOver<PessoaDisciplina>().Where(x => x.Pessoa.Id == pessoaId).List();
            List<int> list = new List<int>();
            foreach (PessoaDisciplina i in itens)
            {
                list.Add(i.Disciplina.Id);
            }
            pd.ListaDisciplinas = list.ToArray();

            return pd;
        }

        public void GravarDisciplinas(PessoaDisciplinaVO pessoa)
        {
            try
            {
                IEnumerable<PessoaDisciplina> itens =
                    Session.QueryOver<PessoaDisciplina>().Where(x => x.Pessoa.Id == pessoa.Id).List();

                foreach (PessoaDisciplina i in itens)
                {
                    Session.Delete(i);
                }

                Pessoa e = GetById(pessoa.Id);
                DisciplinaDAO idao = new DisciplinaDAO();

                foreach (int i in pessoa.ListaDisciplinas)
                {
                    Disciplina item = idao.GetById(i);
                    if ((e != null) && (item != null))
                    {
                        Session.Save(new PessoaDisciplina() { Pessoa = e, Disciplina = item });
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        public IEnumerable<PessoaVO> GetListagemAlunosByTurmaId(int turmaId)
        {


            IEnumerable<PessoaVO> retorno =
            Session.CreateQuery("SELECT " +
                    "p.Id as Id, " +
                    "p.Id as PessoaId, " +
                    "p.NomePai as NomePai, " +
                    "p.Nome as Nome, " +
                    "p.NomeMae as NomeMae, " +
                    "p.NomeResponsavel as NomeResponsavel, " +
                    "p.EmailResponsavel as EmailResponsavel, " +
                    "p.DataNascimento as DataNascimento, " +
                    "p.NumeroNIS as NumeroNIS, " +
                    "p.CodigoINEP as CodigoINEP, " +
                    "s.Id as SexoId, " +
                    "ec.Id as EstadoCivilId, " +
                    "ec.Descricao as EstadoCivilDescricao, " +
                    "r.Id as RacaId, " +
                    "r.Descricao as RacaDescricao, " +
                    "p.FlagDeficiencia as FlagDeficiencia, " +
                    "p.FlagTipoPessoa as FlagTipoPessoa, " +
                    "p.NomeConjuge as NomeConjuge " +
            "FROM Matricula m " +
            "INNER JOIN m.Turma t " +
            "INNER JOIN m.Pessoa p " +
            "LEFT JOIN p.EstadoCivil ec " +
            "LEFT JOIN p.Sexo as s " +
            "LEFT JOIN p.Raca as r " +
            "WHERE FlagTipoPessoa = 'A' " +
            "AND t.TurmaId = :turmaId " +
            "ORDER BY p.Nome ")
            .SetParameter("turmaId", turmaId)
            .SetResultTransformer(Transformers.AliasToBean(typeof(PessoaVO)))
            .List<PessoaVO>();

            return retorno;
        }

        public IEnumerable<Pessoa> GetListagem(string tipo, string searchString = null)
        {
            IQueryOver<Pessoa> q = Session.QueryOver<Pessoa>();
            IEnumerable<Pessoa> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<Pessoa>()
                    .Where(s => s.FlagTipoPessoa == tipo)
                    .Where(s => s.Nome.ToLower().Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<Pessoa>().Where(s => s.FlagTipoPessoa == tipo).ToList();
            }
            return lista;
        }

        public IEnumerable<PessoaVO> GetListaPessoas(string tipo, string searchString = null)
        {
            IEnumerable<PessoaVO> retorno =
            Session.CreateQuery("SELECT " +
                    "p.Id as Id, "+
                    "p.Id as PessoaId, "+
                    "p.NomePai as NomePai, "+
                    "p.Nome as Nome, "+
                    "p.NomeMae as NomeMae, "+
                    "p.NomeResponsavel as NomeResponsavel, "+
                    "p.EmailResponsavel as EmailResponsavel, "+
                    "p.DataNascimento as DataNascimento, "+
                    "p.NumeroNIS as NumeroNIS, "+
                    "p.CodigoINEP as CodigoINEP, "+
                    "s.Id as SexoId, "+
                    "ec.Id as EstadoCivilId, "+
                    "ec.Descricao as EstadoCivilDescricao, "+
                    "r.Id as RacaId, "+
                    "r.Descricao as RacaDescricao, "+
                    "p.FlagDeficiencia as FlagDeficiencia, "+
                    "p.FlagTipoPessoa as FlagTipoPessoa, "+
                    "p.NomeConjuge as NomeConjuge "+
            "FROM Pessoa p " +
            "LEFT JOIN p.EstadoCivil ec " +
            "LEFT JOIN p.Sexo as s " +
            "LEFT JOIN p.Raca as r " +
            "WHERE p.FlagTipoPessoa = :tipo "+
            "ORDER BY p.Nome ")
            .SetParameter("tipo", tipo)
            .SetResultTransformer(Transformers.AliasToBean(typeof(PessoaVO)))
            .List<PessoaVO>();
            
            return retorno;
        }

        public PessoaVO GetPessoaVOById(int id)
        {
        	PessoaVO model = Session.CreateQuery(
        		"SELECT "+
                "tb.Id as Id, "+
                "tb.Id as PessoaId, "+
                "tb.NomePai as NomePai, "+
                "tb.Nome as Nome, "+
                "tb.NomeMae as NomeMae, "+
                "tb.NomeResponsavel as NomeResponsavel, "+
                "tb.EmailResponsavel as EmailResponsavel, "+
                "tb.DataNascimento as DataNascimento, "+
                "tb.NumeroNIS as NumeroNIS, "+
                "tb.CodigoINEP as CodigoINEP, "+
                "s.Id as SexoId, "+
                "ec.Id as EstadoCivilId, "+
                "ec.Descricao as EstadoCivilDescricao, "+
                "tn.Id as TipoNacionalidadeId, "+
                "tn.Descricao as TipoNacionalidadeDescricao, "+
                "p.Id as PaisOrigemId, "+
                "r.Id as RacaId, "+
                "uf.Id as UFNascimentoId, "+
                "cdd.Id as CidadeNascimentoId, "+
                "tb.FlagDeficiencia as FlagDeficiencia, "+
                "tb.FlagTipoPessoa as FlagTipoPessoa, "+
                "tb.NomeConjuge as NomeConjuge "+
        		"FROM Pessoa tb "+
        		"LEFT JOIN tb.EstadoCivil ec  "+
        		"LEFT JOIN tb.TipoNacionalidade tn  "+
        		"LEFT JOIN tb.Sexo s  "+
        		"LEFT JOIN tb.PaisOrigem p  "+
        		"LEFT JOIN tb.Raca r "+
        		"LEFT JOIN tb.UFNascimento uf "+
        		"LEFT JOIN tb.CidadeNascimento cdd "+
        	    "WHERE tb.Id = :id")
        		.SetParameter("id",id)
        		.SetResultTransformer(Transformers.AliasToBean(typeof(PessoaVO)))
        		.UniqueResult<PessoaVO>();
        	
        	return model;
        	
        	/*
            PessoaVO avo = null;
            Pessoa a = null;
            Sexo s = null;
            Raca r = null;
            TipoNacionalidade tn = null;
            Pais p = null;
            EstadoCivil ec = null;
            Estado uf = null;
            Municipio cdd = null;

            PessoaVO model =
            Session.QueryOver<Pessoa>(() => a)
                .Left.JoinQueryOver<EstadoCivil>(() => a.EstadoCivil, () => ec)
                .Left.JoinQueryOver<TipoNacionalidade>(() => a.TipoNacionalidade, () => tn)
                .Left.JoinQueryOver<Sexo>(() => a.Sexo, () => s)
                .Left.JoinQueryOver<Pais>(() => a.PaisOrigem, () => p)
                .Left.JoinQueryOver<Raca>(() => a.Raca, () => r)

                .Left.JoinQueryOver<Estado>(() => a.UFNascimento, () => uf)
                .Left.JoinQueryOver<Municipio>(() => a.CidadeNascimento, () => cdd)

                .SelectList(list => list
                    .Select(() => a.Id).WithAlias(() => avo.Id)
                    .Select(() => a.Id).WithAlias(() => avo.PessoaId)
                    .Select(() => a.NomePai).WithAlias(() => avo.NomePai)
                    .Select(() => a.Nome).WithAlias(() => avo.Nome)
                    .Select(() => a.NomeMae).WithAlias(() => avo.NomeMae)
                    .Select(() => a.NomeResponsavel).WithAlias(() => avo.NomeResponsavel)
                    .Select(() => a.EmailResponsavel).WithAlias(() => avo.EmailResponsavel)
                    .Select(() => a.DataNascimento).WithAlias(() => avo.DataNascimento)
                    .Select(() => a.NumeroNIS).WithAlias(() => avo.NumeroNIS)
                    .Select(() => a.CodigoINEP).WithAlias(() => avo.CodigoINEP)
                    .Select(() => s.Id).WithAlias(() => avo.SexoId)
                    .Select(() => ec.Id).WithAlias(() => avo.EstadoCivilId)
                    .Select(() => ec.Descricao).WithAlias(() => avo.EstadoCivilDescricao)
                    .Select(() => tn.Id).WithAlias(() => avo.TipoNacionalidadeId)
                    .Select(() => tn.Descricao).WithAlias(() => avo.TipoNacionalidadeDescricao)
                    .Select(() => p.Id).WithAlias(() => avo.PaisOrigemId)
                    .Select(() => r.Id).WithAlias(() => avo.RacaId)
                    .Select(() => uf.Id).WithAlias(() => avo.UFNascimentoId)
                    .Select(() => cdd.Id).WithAlias(() => avo.CidadeNascimentoId)
                    .Select(() => a.FlagDeficiencia).WithAlias(() => avo.FlagDeficiencia)
                    .Select(() => a.FlagTipoPessoa).WithAlias(() => avo.FlagTipoPessoa)
                    .Select(() => a.NomeConjuge).WithAlias(() => avo.NomeConjuge)
                ).Where(() => a.Id == id)
                .TransformUsing(Transformers.AliasToBean<PessoaVO>())
                .SingleOrDefault<PessoaVO>();

            return model;
            */
        }


    } // END CLASS
} // END NAMESPACE
