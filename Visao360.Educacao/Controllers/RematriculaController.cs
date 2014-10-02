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

        //[Acesso(AcaoId = "rematricula.etapas")]
        public ActionResult Etapas(int modalidadeId)
        {
            IEnumerable<ItemVO> lista = ItemVOBuilders.Instance.BuildListaEtapa(modalidadeId);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OldEtapas(int modalidadeId)
        {
            IEnumerable<ItemVO> lista = ItemVOBuilders.Instance.BuildListaEtapa(modalidadeId);

            string html = "";
            foreach (ItemVO i in lista)
            {
                html = html + "<option value='" + i.Id + "'>" + i.Descricao + "</option>";
            }

            return this.Content(html, "text/html", System.Text.Encoding.UTF8);
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

        [Acesso(AcaoId = "rematricula.alunos")]
        public ActionResult Alunos(int turmaOrigemId)
        {
            MatriculaDAO mdao = new MatriculaDAO();
            IEnumerable<MatriculaVO> lista = mdao.GetListaMatriculasParaRematricular(turmaOrigemId);

            // PessoaId, PessoaNome

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [Acesso(AcaoId = "rematricula.selecionar")]
        [SelecionouFilial(MensagemErro = "Para acessar Rematrícula, selecione primeiro uma Escola Padrão.")]
        public ActionResult Selecionar(int id = 0)
        {
            /*
            if (!ExisteEscolaSelecionada("Para acessar Rematrícula, selecione primeiro uma Escola Padrão"))
            {
                return RedirectToAction("Selecionar", "Home");
            };
            */
            TurmaVO t = new TurmaDAO().GetVOById(id);
            RematriculaVO model = new RematriculaVO();
            if (t != null)
            {
                model.TurmaDestinoId = t.Id;
            }
            ViewBag.Turma = t;
            ViewBag.Acao = "Seleção de Alunos para Rematrícula";
            EnviarViewBagEdit();

            return View(model);
        }

        private void EnviarViewBagEdit() {
            IEnumerable<TurmaVO> lista = new TurmaDAO().GetListagemByEscolaAno(this.EscolaSessao.EscolaId, this.EscolaSessao.AnoLetivoAno - 1);
            ViewBag.ListaTurma = lista;
            ViewBag.ListaEscolarizacaoEspecial = ComboBuilder.ListaEscolarizacaoEspecial();
            ViewBag.ListaTransportePublico = ComboBuilder.ListaTransportePublico();
            ViewBag.ListaTurmaUnificada = ComboBuilder.ListaTurmaUnificada();
            ViewBag.ListaSimNao = ComboBuilder.ListaSimNao();
        }

        [HttpPost, ActionName("Selecionar")]
        [Persistencia]
        public ActionResult EditConfirmed(RematriculaVO model)
        {
            // Se não é válido, retorna
            if (!ModelState.IsValid)
            {
                ViewBag.Acao = "Seleção de Alunos para Rematrícula";
                EnviarViewBagEdit();
                TurmaVO t = new TurmaDAO().GetVOById(model.TurmaDestinoId);
                ViewBag.Turma = t;
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
    }
}
