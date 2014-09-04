using Dardani.EDU.BO.NH;
using Dardani.EDU.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Visao360.Educacao.Models;

namespace Visao360.Educacao.Helpers
{
    public class GerenciadorEscolaSessao
    {
        private const string VARIAVEL = "escola";

        public static EscolaSessao GetEscolaAtual()
        {
            if (HttpContext.Current.Session[VARIAVEL] == null)
            {
                return null;
            } else {
                return (EscolaSessao)HttpContext.Current.Session[VARIAVEL];
            }
        }

        public static void Limpar() {
            HttpContext.Current.Session[VARIAVEL] = null;
        }

        public static EscolaSessao SetEscola(EscolaSessao e)
        {
            EscolaSessao retorno = null;
            if (HttpContext.Current.Session[VARIAVEL] != null) {
                EscolaSessao model = (EscolaSessao)HttpContext.Current.Session[VARIAVEL];
                
                // Pode ser que mude só a escola
                if (e.AnoLetivoId == 0) {
                    e.AnoLetivoId = model.AnoLetivoId;
                }

                if ((model.EscolaId != e.EscolaId) || (model.AnoLetivoId != e.AnoLetivoId))
                {
                    retorno = validarEscolaSessao(e);
                    HttpContext.Current.Session[VARIAVEL] = retorno; //dao.GetCemiterioById(id);
                }
            } else {
                retorno = validarEscolaSessao(e);
                HttpContext.Current.Session[VARIAVEL] = retorno; // ConstrucaoServices.Instance.GetCemiterioById(id);
            }
            //@TempData["mensagem"] = string.Format("Escola {0} é atual escola de trabalho. Ano Letivo = {1}", retorno.EscolaNome, retorno.AnoLetivoAno);
            return retorno;
        }

        private static EscolaSessao validarEscolaSessao(EscolaSessao entrada) {
            AnoLetivoDAO adao = new AnoLetivoDAO();
            EscolaDAO edao = new EscolaDAO();
            EscolaSessao model = new EscolaSessao();
            AnoLetivo a = adao.GetById(entrada.AnoLetivoId);

            model.EscolaId = entrada.EscolaId;
            model.AnoLetivoId = entrada.AnoLetivoId;

            if (a!= null){
                model.AnoLetivoAno = a.Ano;
            }

            Escola e = edao.GetById(entrada.EscolaId);
            if (e != null) {
                model.EscolaNome = e.Nome;
            }

            return model;
        }
    }
}
