using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Visao360.Educacao
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "TheMatriz",
                url: "Matriz/{modalidadeId}/{etapaId}",
                defaults: new
                {
                    controller = "Matrizes"
                    ,action = "Matriz"
                });

            routes.MapRoute(
                name: "MatrizSelecionar",
                url: "MatrizSelecionar",
                defaults: new
                {
                    controller = "Matrizes",
                    action = "MatrizSelecionar"
                });

            routes.MapRoute(
                name: "TheMatrizDisciplina",
                url: "MatrizDisciplina/{modalidadeId}/{etapaId}/{identificacao}",
                defaults: new
                {
                    controller = "Matrizes",
                    action = "MatrizDisciplina",
                    identificacao = UrlParameter.Optional
                });





            routes.MapRoute(
                name: "PeriodoHorarioIndex",
                url: "PeriodosHorario/{id}",
                defaults: new
                {
                    controller = "Horarios",
                    action = "Periodos"
                });

            routes.MapRoute(
                name: "PeriodoHorarioEdit",
                url: "PeriodoHorario/{horarioId}/Edit/{periodoId}",
                defaults: new
                {
                    controller = "Horarios",
                    action = "PeriodoEdit", 
                    periodoId = UrlParameter.Optional
                });


            routes.MapRoute(
                name: "PeriodoHorarioDelete",
                url: "PeriodoHorario/{horarioId}/Delete/{periodoId}",
                defaults: new
                {
                    controller = "Horarios",
                    action = "PeriodoDelete",
                    periodoId = UrlParameter.Optional
                });






            routes.MapRoute(
                name: "CalendariosEventos",
                url: "Calendarios/{calendarioId}/Eventos/{mesId}",
                defaults: new
                {
                    controller = "Calendarios",
                    action = "Eventos"
                });


            /*
            routes.MapRoute(
                name: "EditarAluno",
                url: "Alunos/Edit",
                defaults: new
                {
                    controller = "Pessoas",
                    action = "Edit",
                    tipo = "A",
                    Id = UrlParameter.Optional,
                }
            );
            */
            /*
            routes.MapRoute(
                name: "EditarProfissional",
                url: "Profissionais/Edit",
                defaults: new
                {
                    controller = "Pessoas",
                    action = "Edit",
                    tipo = "P",
                    Id = UrlParameter.Optional,
                }
            );
            */
            /*
            routes.MapRoute(
                name: "ListagemAlunos",
                url: "Alunos",
                defaults: new
                {
                    controller = "Pessoas",
                    action = "Index",
                    tipo = "A",
                    searchstring = UrlParameter.Optional,
                }
            );
            */
            /*
            routes.MapRoute(
                name: "ListagemProfissionais",
                url: "Profissionais",
                defaults: new
                {
                    controller = "Pessoas",
                    action = "Index",
                    tipo = "P",
                    searchstring = UrlParameter.Optional,
                }
            );
            */
            routes.MapRoute(
                name: "AlunoNecessidadesEspeciaisEdit",
                url: "Alunos/NE/{id}/NId/{nId}",
                defaults: new
                {
                    controller = "Alunos",
                    action = "NecessidadesEspeciaisEdit",
                    nId = UrlParameter.Optional,
                }
            );

            routes.MapRoute(
                name: "DisciplinasTurma",
                url: "TurmaDisciplinas/{turmaId}/{action}/{id}",
                defaults: new
                {
                    controller = "Turmas", // Regencias
                    action = "Disciplinas",
                    turmaId = UrlParameter.Optional,
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "AlunosTurma",
                url: "TurmaAlunos/{turmaId}/{action}/{id}",
                defaults: new
                {
                    controller = "Turmas", // Regencias
                    action = "Alunos",
                    turmaId = UrlParameter.Optional,
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "HorariosTurma",
                url: "HT/{turmaId}/{action}/{id}",
                defaults: new
                {
                    controller = "RegenciasHorarios",
                    action = "Index",
                    turmaId = UrlParameter.Optional,
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}