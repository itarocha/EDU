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
    public class TurmaDAO : GenericDAO<Turma>
    {

        public Turma GetByCodigoINEP(string codigo)
        {
            Turma value = Session.QueryOver<Turma>()
                .Where(x => x.CodigoINEP == codigo)
                .List().FirstOrDefault();
            return value;
        }

        public IEnumerable<Turma> GetByEscolaAnoAluno(int escolaId, int anoLetivoId, int pessoaId) {
        	
        	IEnumerable<Turma> model = Session.CreateQuery(
        		"SELECT t "+
        		"FROM Matricvula m "+
                "INNER JOIN m.Turma t "+
                "INNER JOIN m.Pessoa p "+
                "INNER JOIN t.Escola e "+
                "INNER JOIN t.Calendario c "+
                "INNER JOIN c.AnoLetivo a "+
                "WHERE p.Id = :pessoaId "+
                "AND e.Id = :escolaId "+
                "AND a.Id = :anoLetivoId ")
        		.SetParameter("pessoaId", pessoaId)
        		.SetParameter("escolaId", escolaId)
        		.SetParameter("anoLetivoId", anoLetivoId)
        		.SetResultTransformer(Transformers.AliasToBean(typeof(Turma)))
        		.List<Turma>();
        	
        	return model;
        }

        public IEnumerable<Turma> GetListagem(string searchString = null)
        {
            IQueryOver<Turma> q = Session.QueryOver<Turma>();
            IEnumerable<Turma> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<Turma>()
                    .Where(s => s.Nome.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<Turma>().ToList();
            }
            return lista;
        }

        public IEnumerable<TurmaVO> GetListagemByEscolaAno(int escolaId, int anoLetivo)
        {

        	IEnumerable<TurmaVO> model = Session.CreateQuery(
        		"SELECT "+
                "t.Id as Id, "+
                "t.Nome as Nome, "+
                "t.CodigoINEP as CodigoINEP, "+
                "t.FlagDomingo as FlagDomingo, "+
                "t.FlagSegunda as FlagSegunda, "+
                "t.FlagTerca as FlagTerca, "+
                "t.FlagQuarta as FlagQuarta, "+
                "t.FlagQuinta as FlagQuinta, "+
                "t.FlagSexta as FlagSexta, "+
                "t.FlagSabado as FlagSabado, "+
                "t.FlagPrograma as FlagPrograma, "+
                "e.Id as EscolaId, "+
                "e.Nome as EscolaNome, "+
                "c.Id as CalendarioId, "+
                "c.Descricao as CalendarioDescricao, "+
                "al.Ano as CalendarioAno, "+
                "m.Id as ModalidadeId, "+
                "m.Descricao as ModalidadeDescricao, "+
                "etp.Id as EtapaId, "+
                "te.Descricao as TipoEnsinoDescricao, "+
                "se.Descricao as SerieDescricao, "+
                "m.Id as ModalidadeId, "+
                "m.Descricao as ModalidadeDescricao, "+
                "trn.Id as TurnoId, "+
                "trn.Descricao as TurnoDescricao, " +
                "hor.Descricao as HorarioDescricao, " +
                "hor.HoraInicial as HorarioHoraInicial, " +
                "hor.HoraFinal as HorarioHoraFinal, "+
                "s.Id as SalaId, "+
                "s.Descricao as SalaDescricao, "+
                "ta.Id as TipoAtendimentoId, "+
                "ta.Descricao as TipoAtendimentoDescricao "+
				"FROM Turma t "+
	            "INNER JOIN t.Escola e "+
	            "INNER JOIN t.Calendario c "+
	            "INNER JOIN c.AnoLetivo al "+
	            "LEFT JOIN t.Modalidade m "+
	            "LEFT JOIN t.Etapa etp "+
	            "LEFT JOIN etp.TipoEnsino te "+
	            "LEFT JOIN etp.Serie se "+
                "INNER JOIN t.Turno trn " +
                "LEFT JOIN t.Horario hor " +
                "LEFT JOIN t.Sala s "+
	            "INNER JOIN t.TipoAtendimento ta "+
	            "WHERE e.Id = :escolaId "+
	            "AND al.Ano = :anoLetivo "
        	)
        	.SetParameter("escolaId",escolaId)
        	.SetParameter("anoLetivo",anoLetivo)
        	.SetResultTransformer(Transformers.AliasToBean(typeof(TurmaVO)))
        	.List<TurmaVO>();
        	
        	return model;
        }

        public TurmaVO GetVOById(int id)
        {
        	TurmaVO model = Session.CreateQuery(
        		"SELECT "+
                "t.Id as Id, "+
                "t.Nome as Nome, "+
                "t.CodigoINEP as CodigoINEP, "+
                "t.FlagDomingo as FlagDomingo, "+
                "t.FlagSegunda as FlagSegunda, "+
                "t.FlagTerca as FlagTerca, "+
                "t.FlagQuarta as FlagQuarta, "+
                "t.FlagQuinta as FlagQuinta, "+
                "t.FlagSexta as FlagSexta, "+
                "t.FlagSabado as FlagSabado, "+
                "t.FlagPrograma as FlagPrograma, "+
                "e.Id as EscolaId, "+
                "e.Nome as EscolaNome, "+
                "c.Id as CalendarioId, "+
                "c.Descricao as CalendarioDescricao, "+
                "al.Ano as CalendarioAno, "+
                "m.Id as ModalidadeId, "+
                "m.Descricao as ModalidadeDescricao, "+
                "etp.Id as EtapaId, "+
                "te.Descricao as TipoEnsinoDescricao, "+
                "se.Descricao as SerieDescricao, "+
                "m.Id as ModalidadeId, "+
                "m.Descricao as ModalidadeDescricao, "+
                "trn.Id as TurnoId, " +
                "trn.Descricao as TurnoDescricao, " +
                "hor.Id as HorarioId, " +
                "hor.Descricao as HorarioDescricao, " +
                "hor.HoraInicial as HorarioHoraInicial, " +
                "hor.HoraFinal as HorarioHoraFinal, " +
                "s.Id as SalaId, "+
                "s.Descricao as SalaDescricao, "+
                "ta.Id as TipoAtendimentoId, "+
                "ta.Descricao as TipoAtendimentoDescricao "+
				"FROM Turma t "+
	            "INNER JOIN t.Escola e "+
	            "INNER JOIN t.Calendario c "+
	            "INNER JOIN c.AnoLetivo al "+
	            "LEFT JOIN t.Modalidade m "+
	            "LEFT JOIN t.Etapa etp "+
	            "INNER JOIN etp.TipoEnsino te "+
	            "INNER JOIN etp.Serie se "+
                "INNER JOIN t.Turno trn " +
                "LEFT JOIN t.Horario hor " +
                "LEFT JOIN t.Sala s " +
	            "INNER JOIN t.TipoAtendimento ta "+
	            "WHERE t.Id = :turmaId "
        	)
        	.SetParameter("turmaId",id)
        	.SetResultTransformer(Transformers.AliasToBean(typeof(TurmaVO)))
        	.UniqueResult<TurmaVO>();
        	
        	return model;
        }
    } // END CLASS
} // END NAMESPACE
