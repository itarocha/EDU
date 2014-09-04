using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visao360.Educacao.Filters;
using Visao360.Educacao.Helpers;
using Dardani.EDU.Entities.Model;
using Dardani.EDU.BO.NH;
using Petra.Util.Model;
using Petra.Util.Funcoes;
using Dardani.EDU.Entities.VO;
using Dardani.EDU.BO.App;
using Visao360.Educacao.Models;
using Petra.DAO.Util;

namespace Visao360.Educacao.Controllers
{
    public class RematriculaController : BaseController
    {

        [Role(Roles = "Administrador,Visitante")]
        public ActionResult Etapas(int modalidadeId)
        {
            IEnumerable<ItemVO> lista = ItemVOBuilders.Instance.BuildListaEtapa(modalidadeId);

            /*
            string html = "";
            foreach (ItemVO i in lista)
            {
                html = html + "<option value='" + i.Id + "'>" + i.Descricao + "</option>";
            }

            return this.Content(html, "text/html", System.Text.Encoding.UTF8);
            */

            return Json(lista, JsonRequestBehavior.AllowGet);


            //return html;
            //var retorno = Json(lista, JsonRequestBehavior.AllowGet);


            //return retorno;
        }


        [Role(Roles = "Administrador,Visitante")]
        public ActionResult OldEtapas(int modalidadeId)
        {
            IEnumerable<ItemVO> lista = ItemVOBuilders.Instance.BuildListaEtapa(modalidadeId);

            string html = "";
            foreach (ItemVO i in lista)
            {
                html = html + "<option value='" + i.Id + "'>" + i.Descricao + "</option>";
            }

            return this.Content(html, "text/html", System.Text.Encoding.UTF8);


            //return html;
            //var retorno = Json(lista, JsonRequestBehavior.AllowGet);


            //return retorno;
        }

        /*
        public function ajaxbuscavagasAction() {
            // Inicia conexao com o banco de dados 
            $this->noRender();
            if ($this->_getParam('cod_vaga_empresa') != '') {
                $rsVaga = $this->buscaVagas($this->_getParam('cod_vaga_empresa'));
                if (count($rsVaga) > 0) {
                    $html = '<option value="">Selecione uma opção ...</option>';
                    foreach ($rsVaga as $value) {
                        $html .= '<option value="' . $value['cod_vaga'] . '">' . $value['tit_vaga'] . '</option>';
                    }
                } else {
                    $html = '<option value="">Nenhum registro encontrado!</option>';
                }
            } else {
                $html = '<option value="">Selecione uma opção ...</option>';
            }
            echo $html;
        }
        */

        [Role(Roles = "Administrador,Visitante")]
        public ActionResult Alunos(int turmaOrigemId)
        {
            MatriculaDAO mdao = new MatriculaDAO();
            IEnumerable<MatriculaVO> lista = mdao.GetListaMatriculasParaRematricular(turmaOrigemId);

            // PessoaId, PessoaNome

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [Role(Roles = "Administrador")]
        public ActionResult Selecionar(int id = 0)
        {
            EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            int escolaId = (e == null) ? 0 : e.EscolaId;
            // Se escola for 0, redirecionar para index de Escolas e enviar mensagem
            if (escolaId == 0)
            {
                TempData["mensagem"] = "Para gerenciar Rematrículas é necessário tornar uma Escola Padrão";
                return RedirectToAction("Selecionar", "Home"); // RedirectToAction("Index", controllerName: "Escolas");
            }
            TurmaVO t = new TurmaDAO().GetVOById(id);
            RematriculaVO model = new RematriculaVO();
            if (t != null)
            {
                model.TurmaDestinoId = t.Id;
            }
            IEnumerable<TurmaVO> lista = new TurmaDAO().GetListagemByEscolaAno(escolaId, e.AnoLetivoAno - 1);
            ViewBag.Acao = "Seleção de Alunos para Rematrícula";
            ViewBag.ListaTurma = lista;
            ViewBag.Turma = t;
            EnviarViewBagEdit();

            return View(model);
        }

        private void EnviarViewBagEdit() {
            ViewBag.ListaEscolarizacaoEspecial = ComboBuilder.ListaEscolarizacaoEspecial();
            ViewBag.ListaTransportePublico = ComboBuilder.ListaTransportePublico();
            ViewBag.ListaTurmaUnificada = ComboBuilder.ListaTurmaUnificada();
            ViewBag.ListaSimNao = ComboBuilder.ListaSimNao();
        }

        [HttpPost, ActionName("Selecionar")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult EditConfirmed(RematriculaVO model)
        {
            // Se não é válido, retorna
            if (!ModelState.IsValid)
            {
                ViewBag.Acao = "Seleção de Alunos para Rematrícula";
                EnviarViewBagEdit();
                return View(model);
            }
            EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            MatriculaDAO dao = new MatriculaDAO();
            foreach(int pessoaId in model.ListaAlunos) {
                MatriculaVO mvo = new MatriculaVO() { 
                    PessoaId = pessoaId, 
                    AnoLetivoId = e.AnoLetivoId,
                    TurmaId = model.TurmaDestinoId,
                    EscolarizacaoEspecialId = model.EscolarizacaoEspecialId,
                    FlagRematricular = "S",
                    TransportePublicoId = model.TransportePublicoId, 
                    TurmaUnificadaId = model.TurmaUnificadaId
                };
                Matricula toSave = new Matricula();
                Conversor.Converter(mvo, toSave, NHibernateBase.Session);
                dao.SaveOrUpdate(toSave, toSave.Id);    
            }
            return Redirect(string.Format("/TurmaAlunos/{0}",model.TurmaDestinoId));
        }

        /*
        public ActionResult Disciplinas(int turmaId)
        {
            ViewBag.TurmaId = turmaId;

            return View();
        }
        */
    }
}
