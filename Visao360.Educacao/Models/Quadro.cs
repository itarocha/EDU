using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dardani.EDU.Entities.VO;
using Dardani.EDU.BO.NH;
using Dardani.EDU.Entities.Model;

namespace Visao360.Educacao.Models
{
    public class Quadro
    {
        //public List<String> Cabecalho {get; set;}
        public List<Coluna> Colunas { get; set; }
        public List<Faixa> Faixas { get; set; }
        //public List<String> Horarios {get; set;}
        public List<Celula> Celulas = new List<Celula>();

        public IEnumerable<TurmaHorarioVO> ListaHorarios { get; set; }

        public int DisciplinaId { get; set; }
        public int PessoaId { get; set; }
        public int TurmaId { get; set; }
        public int HorarioId { get; set; }

        public Quadro(int turmaId)
        {
            this.TurmaId = turmaId;

            Turma t = new TurmaDAO().GetById(turmaId);
            this.HorarioId = t.Horario.Id;


            //Cabecalho = new List<String>{"Aleph", "Beth", "Guimel", "Daleth", "Vav", "Toth", "Zain"};
            //Horarios = new List<String>{"001-005","006-010", "011-015", "016-020"};
            Colunas = new List<Coluna>{
			    new Coluna(){Id = 1, Descricao="Domingo", Selecionavel=false},
			    new Coluna(){Id = 2, Descricao="Segunda", Selecionavel=true},
			    new Coluna(){Id = 3, Descricao="Terça", Selecionavel=true},
			    new Coluna(){Id = 4, Descricao="Quarta", Selecionavel=true},
			    new Coluna(){Id = 5, Descricao="Quinta", Selecionavel=true},
			    new Coluna(){Id = 6, Descricao="Sexta", Selecionavel=true},
			    new Coluna(){Id = 7, Descricao="Sábado", Selecionavel=false},
		    };


            Faixas = new List<Faixa>();

            HorarioPeriodoDAO tpdao = new HorarioPeriodoDAO();

            // Tá errado. O parâmetro não é TurnoId, mas HorárioId... Tem que mudar isso
            IEnumerable<HorarioPeriodoVO> periodos = tpdao.GetByHorarioId(this.HorarioId);
            foreach (HorarioPeriodoVO tp in periodos) {
                //tp.Id,
                //tp.HoraInicio,
                //tp.HoraTermino,
                //tp.FaixaHorario

                Faixas.Add(new Faixa() { 
                    Id = tp.Id, 
                    HoraInicio = tp.HoraInicio, 
                    HoraTermino = tp.HoraTermino, 
                    FaixaHorario = tp.FaixaHorario
                });                
            }

            //q.Periodos = periodos;

            /*
            {
			    new Faixa(){Id = 1, ValorIni="001", ValorFim = "005"},
			    new Faixa(){Id = 2, ValorIni="006", ValorFim = "010"},
			    new Faixa(){Id = 3, ValorIni="011", ValorFim = "015"},
			    new Faixa(){Id = 4, ValorIni="016", ValorFim = "020"},
			    new Faixa(){Id = 5, ValorIni="021", ValorFim = "025"},
		    };
             */

            TurmaHorarioDAO thdao = new TurmaHorarioDAO();
            this.ListaHorarios = thdao.GetListagemByTurma(turmaId);

        }

   }

    public class Coluna
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Selecionavel { get; set; }
    }

    public class Faixa
    {
        public int Id { get; set; }
        public string HoraInicio { get; set; }
        public string HoraTermino { get; set; }
        public string FaixaHorario { get; set; }

        public override string ToString()
        {
            return String.Format("{0} - {1}", this.HoraInicio, this.HoraTermino);
        }
    }

    public class Celula
    {
        public int CabecalhoId { get; set; }
        public int HorarioId { get; set; }
        public string Nome { get; set; }
    }


    public class RetornoQuadro
    {
        public int TurmaId { get; set; }
        public int PessoaId { get; set; }
        public int DisciplinaId { get; set; }
        public string[] horarios { get; set; }
    }
 
}