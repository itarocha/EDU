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
using Dardani.EDU.Entities.VO;
using Petra.Util.Model;
using Dardani.GER.BO.NH;
using Petra.Util.Funcoes;
using Dardani.EDU.BO.App;
using Petra.DAO.Util;
using Visao360.Educacao.Models;
using System.Globalization;

namespace Visao360.Educacao.Controllers
{
    public class AlunosController : BaseController
    {
        [Role(Roles = "Administrador")]
        public ActionResult Index(string searchString)
        {
            string tipo = "A";
            ViewBag.Titulo = "Listagem de Alunos";
            ViewBag.Tipo = tipo;

            IEnumerable<PessoaVO> lista = new PessoaDAO().GetListaPessoas(tipo, searchString);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Listagem", lista);
            }

            return View(lista);
        }

        [Role(Roles = "Administrador")]
        public ActionResult Edit(int id = 0)
        {
            Boolean novo = (id == 0);

            MatriculaDAO mdao = new MatriculaDAO();
            Matricula m = null;
            PessoaVO model = new PessoaVO();
            if (!novo)
            {
                m = mdao.GetById(id);
                if (m != null)
                {
                    model = new PessoaDAO().GetPessoaVOById(m.Pessoa.Id);
                }
            }
            ViewBag.TurmaId = m == null ? 0 : m.Turma.Id;
            ViewBag.TurmaNome = m == null ? "" : m.Turma.Nome;
            ViewBag.MatriculaId = m == null ? 0 : m.Id;

            if (model == null)
            {
                return HttpNotFound();
            }
            // Popula a seção com os combos

            EnviarViewBagEdit();

            if (novo)
            {
                model.FlagTipoPessoa = "A";
                model.FlagDeficiencia = "N";
            }
            else
            {
                EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            }
            ViewBag.Tipo = "A";
            ViewBag.Acao = novo ? "Novo Aluno" : "Editar Aluno";
            ViewBag.TipoPessoa = "Aluno";
            return View(model);
        }

        private void EnviarViewBagEdit() {
            ViewBag.ListaSexo = ComboBuilder.ListaSexo();
            ViewBag.ListaEstadoCivil = ComboBuilder.ListaEstadoCivil();
            ViewBag.ListaDeficiencia = ComboBuilder.ListaSimNao();
            ViewBag.ListaRaca = ComboBuilder.ListaRaca();
            ViewBag.ListaUF = ComboBuilder.ListaUF();
            ViewBag.ListaCidade = ComboBuilder.ListaCidade();
            ViewBag.ListaPais = ComboBuilder.ListaPais();
            ViewBag.ListaTipoNacionalidade = ComboBuilder.ListaTipoNacionalidade();
        }

        [HttpPost, ActionName("Edit")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult EditConfirmed(PessoaVO model)
        {
            Boolean novo = (model.PessoaId == 0);

            if (!ModelState.IsValid)
            {
                EnviarViewBagEdit();
                Turma t = null;
                t = (model.TurmaId > 0) ? new TurmaDAO().GetById(model.TurmaId) : new Turma();
                ViewBag.Turma = t;
                ViewBag.TurmaId = t.Id;
                ViewBag.TurmaNome = t.Nome;
                ViewBag.Acao = novo ? "Novo Aluno" : "Editar Aluno";
                return View(model);
            }

            // Pega o repositório
            PessoaDAO adao = new PessoaDAO();
            Pessoa aToSave = novo ? new Pessoa() : adao.GetById(model.PessoaId);

            // Converte VO para Entity
            Conversor.Converter(model, aToSave, NHibernateBase.Session);

            // Grava
            adao.SaveOrUpdate(aToSave, aToSave.Id);

            // Redireciona
            if (model.Id > 0)
            {
                //return Redirect(string.Format("/TurmaAlunos/{0}", model.TurmaId));
                return Redirect(string.Format("/Alunos/Endereco/{0}", model.Id));
            }
            else
            {
                return RedirectToAction("Index", "Turmas");
            }
        }

        [Role(Roles = "Administrador")]
        // id será turmaId
        public ActionResult Novo(int id = 0)
        {
            TurmaVO t = new TurmaDAO().GetVOById(id);

            MatriculaDAO mdao = new MatriculaDAO();
            Matricula m = new Matricula();

            PessoaVO model = new PessoaVO();

            /*
            if (!novo)
            {
                m = mdao.GetById(id);
                if (m != null)
                {
                    model = new PessoaDAO().GetPessoaVOById(m.Pessoa.Id);
                }
            }
            */
            ViewBag.Turma = t;
            ViewBag.TurmaId = t == null ? 0 : t.Id;
            ViewBag.TurmaNome = t == null ? "" : t.Nome;
            ViewBag.MatriculaId = 0;

            if (model == null)
            {
                return HttpNotFound();
            }
            // Popula a seção com os combos
            EnviarViewBagNovo();

            model.TurmaId = id;
            model.FlagTipoPessoa = "A";
            model.FlagDeficiencia = "N";

            ViewBag.Tipo = "A";
            ViewBag.Acao = "Novo Aluno";
            ViewBag.TipoPessoa = "Aluno";
            return View(model);
        }

        private void EnviarViewBagNovo() {
            ViewBag.ListaSexo = ComboBuilder.ListaSexo();
            ViewBag.ListaEstadoCivil = ComboBuilder.ListaEstadoCivil();
            ViewBag.ListaDeficiencia = ComboBuilder.ListaSimNao();
            ViewBag.ListaRaca = ComboBuilder.ListaRaca();
            ViewBag.ListaUF = ComboBuilder.ListaUF();
            ViewBag.ListaCidade = ComboBuilder.ListaCidade();
            ViewBag.ListaPais = ComboBuilder.ListaPais();
            ViewBag.ListaTipoNacionalidade = ComboBuilder.ListaTipoNacionalidade();
            ViewBag.ListaEscolarizacaoEspecial = ComboBuilder.ListaEscolarizacaoEspecial();
            ViewBag.ListaTransportePublico = ComboBuilder.ListaTransportePublico();
            ViewBag.ListaTurmaUnificada = ComboBuilder.ListaTurmaUnificada();
        }

        [HttpPost, ActionName("Novo")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult NovoConfirmed(PessoaVO model)
        {
            // Verifica se é novo
            Boolean novo = (model.PessoaId == 0);

            // Se não é válido, retorna
            if (!ModelState.IsValid)
            {
                EnviarViewBagNovo();
                TurmaVO t = null;
                t = (model.TurmaId > 0) ? new TurmaDAO().GetVOById(model.TurmaId) : new TurmaVO();
                ViewBag.Turma = t;
                ViewBag.TurmaId = t.Id;
                ViewBag.TurmaNome = t.Nome;
                ViewBag.Acao = novo ? "Novo Aluno" : "Editar Aluno";
                return View(model);
            }

            // Pega o repositório
            PessoaDAO adao = new PessoaDAO();
            Pessoa aToSave = novo ? new Pessoa() : adao.GetById(model.PessoaId);

            // Converte VO para Entity
            Conversor.Converter(model, aToSave, NHibernateBase.Session);

            // Grava
            adao.SaveOrUpdate(aToSave, aToSave.Id);

            EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            MatriculaDAO dao = new MatriculaDAO();
            MatriculaVO mvo = new MatriculaVO()
            {
                PessoaId = aToSave.Id,
                AnoLetivoId = e.AnoLetivoId,
                TurmaId = model.TurmaId,
                EscolarizacaoEspecialId = model.EscolarizacaoEspecialId,
                FlagRematricular = "S",
                TransportePublicoId = model.TransportePublicoId,
                TurmaUnificadaId = model.TurmaUnificadaId
            };
            Matricula toSave = new Matricula();
            Conversor.Converter(mvo, toSave, NHibernateBase.Session);
            dao.SaveOrUpdate(toSave, toSave.Id);


            // Redireciona
            if (toSave.Id > 0)
            {
                return Redirect(string.Format("/Alunos/Endereco/{0}", toSave.Id));
            }
            else
            {
                return RedirectToAction("Index", "Turmas");
            }
        }

        [Role(Roles = "Administrador")]
        public ActionResult Endereco(int id = 0)
        {
            Boolean novo = (id == 0);
            MatriculaDAO mdao = new MatriculaDAO();
            Matricula m = null;
            PessoaEnderecoVO model = new PessoaEnderecoVO();
            if (!novo)
            {
                m = mdao.GetById(id);
                if (m != null)
                {
                    model = new PessoaEnderecoDAO().GetPessoaEnderecoVOById(m.Pessoa.Id);
                    if (model == null) {
                        model = new PessoaEnderecoVO();
                        model.PessoaId = m.Pessoa.Id;
                    }
                    model.TurmaId = m.Turma.Id;
                }
            }
            ViewBag.TurmaId = m == null ? 0 : m.Turma.Id;
            ViewBag.TurmaNome = m == null ? "" : m.Turma.Nome;
            ViewBag.MatriculaId = m == null ? 0 : m.Id;
            ViewBag.PessoaNome = m == null ? "" : m.Pessoa.Nome;
            if (model == null)
            {
                return HttpNotFound();
            }
            EnviarViewBagEndereco();

            return View(model);
        }

        [HttpPost, ActionName("Endereco")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult EnderecoConfirmed(PessoaEnderecoVO model)
        {
            PessoaEnderecoDAO dao = new PessoaEnderecoDAO();
            PessoaEndereco aToSave = dao.GetById(model.PessoaId);
            Boolean novo = (aToSave == null);
            if (novo)
            {
                aToSave = new PessoaEndereco();
            }

            model.CEP = model.CEP.Replace("-","");
            ModelState.Remove("CEP");
            ModelState.SetModelValue("CEP", new ValueProviderResult(model.CEP, String.Empty, CultureInfo.InvariantCulture));
            if (!ModelState.IsValid)
            {
                EnviarViewBagEndereco();
                return View(model);
            }
            Conversor.Converter(model, aToSave, NHibernateBase.Session);
            dao.SaveOrUpdate(aToSave, aToSave.Id);
            if (model.TurmaId > 0)
            {
                return Redirect(string.Format("/Alunos/Documentacao/{0}", model.Id));
            }
            else
            if (model.TurmaId > 0)
            {
                return Redirect(string.Format("/TurmaAlunos/{0}", model.TurmaId));
            }
            return RedirectToAction("Index", "Turmas");
        }

        private void EnviarViewBagEndereco()
        {
            ViewBag.ListaUF = ComboBuilder.ListaUF();
            ViewBag.ListaCidade = ComboBuilder.ListaCidade();
            ViewBag.ListaZona = ComboBuilder.ListaZona();
        }

        [Role(Roles = "Administrador")]
        public ActionResult Documentacao(int id = 0)
        {
            Boolean novo = (id == 0);
            MatriculaDAO mdao = new MatriculaDAO();
            Matricula m = null;
            PessoaDocumentacaoVO model = new PessoaDocumentacaoVO();
            if (!novo)
            {
                m = mdao.GetById(id);
                if (m != null)
                {
                    model = new PessoaDocumentacaoDAO().GetPessoaDocumentacaoVOById(m.Pessoa.Id);
                    if (model == null) {
                        model = new PessoaDocumentacaoVO();
                        model.PessoaId = m.Pessoa.Id;
                    }
                    model.TurmaId = m.Turma.Id;
                }
            }
            ViewBag.TurmaId = m == null ? 0 : m.Turma.Id;
            ViewBag.TurmaNome = m == null ? "" : m.Turma.Nome;
            ViewBag.MatriculaId = m == null ? 0 : m.Id;
            ViewBag.PessoaNome = m == null ? "" : m.Pessoa.Nome;
            if (model == null)
            {
                return HttpNotFound();
            }

            // Popula a seção com os combos
            EnviarViewBagDocumentacao();

            return View(model);
        }

        private void EnviarViewBagDocumentacao() {
            ViewBag.ListaUF = ComboBuilder.ListaUF();
            ViewBag.ListaCidade = ComboBuilder.ListaCidade();
            ViewBag.ListaZona = ComboBuilder.ListaZona();
            ViewBag.ListaSituacaoDocumento = ComboBuilder.ListaSituacaoDocumento();
            ViewBag.ListaModeloCertidao = ComboBuilder.ListaModeloCertidao();
            ViewBag.ListaTipoCertidao = ComboBuilder.ListaTipoCertidao();
        }

        [HttpPost, ActionName("Documentacao")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult DocumentacaoConfirmed(PessoaDocumentacaoVO model)
        {
            PessoaDocumentacaoDAO dao = new PessoaDocumentacaoDAO();
            PessoaDocumentacao aToSave = dao.GetById(model.PessoaId);
            Boolean novo = (aToSave == null);
            if (novo)
            {
                aToSave = new PessoaDocumentacao();
            }
            if (!ModelState.IsValid)
            {
                EnviarViewBagDocumentacao();
                return View(model);
            }
            model.CPFNumero = model.CPFNumero.Replace(".", "");
            model.CPFNumero = model.CPFNumero.Replace("-", "");
            Conversor.Converter(model, aToSave, NHibernateBase.Session);
            /*
            if (novo)
            {
                aToSave.Pessoa = new PessoaDAO().GetById(model.PessoaId);
            }
             */ 
            dao.SaveOrUpdate(aToSave, aToSave.Id);
            if (model.TurmaId > 0)
            {
                return Redirect(string.Format("/TurmaAlunos/{0}", model.TurmaId));
            }
            else
            {
                return RedirectToAction("Index", "Turmas");
            }
        }

        [Role(Roles = "Administrador")]
        public ActionResult Delete(int Id)
        {
            /*
            DiaSemanaDAO dao = new DiaSemanaDAO();
            DiaSemana model = dao.GetById(Id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
             */
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult DeleteConfirmed(int id)
        {
            /*
            DiaSemanaDAO dao = new DiaSemanaDAO();
            if (ModelState.IsValid)
            {
                DiaSemana o = dao.GetById(id);
                string descricao = o.Descricao;

                dao.Delete(o);

                TempData["mensagem"] = string.Format("DiaSemana {0} excluída com sucesso", descricao);
                return RedirectToAction("Index");
            }
            DiaSemana model = dao.GetById(id);
            return View(model);
             */
            return View();
        }

        [Role(Roles = "Administrador")]
        public ActionResult NecessidadesEspeciaisEdit(int id, int nId)
        {
            /*
            IEnumerable<DiaSemana> lista = new DiaSemanaDAO().GetListagem(searchString);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Listagem", lista);
            }
            return View(lista);
             */
            return View();
        }
    }
}

