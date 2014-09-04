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

namespace Visao360.Educacao.Controllers
{
    public class ProfissionaisController : BaseController
    {
        [Role(Roles = "Administrador")]
        public ActionResult Index(string searchString)
        {
            string tipo = "P";
            ViewBag.Titulo = "Listagem de Profissionais";
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

            PessoaVO model = new PessoaVO();
            if (!novo)
            {
                model = new PessoaDAO().GetPessoaVOById(id);
            }
            if (model == null)
            {
                return HttpNotFound();
            }
            // Popula a seção com os combos

            EnviarViewBagEdit();

            if (novo)
            {
                model.FlagTipoPessoa = "P";
                model.FlagDeficiencia = "N";
            }
            else
            {
                EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            }
            ViewBag.Tipo = "P";
            ViewBag.Acao = novo ? "Novo Profissional" : "Editar Profissional";
            ViewBag.TipoPessoa = "Profissional";
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
            Boolean novo = (model.Id == 0);
            if (!ModelState.IsValid)
            {
                ViewBag.Acao = novo ? "Novo Profissional" : "Editar Profissional";
                EnviarViewBagEdit();
                return View(model);
            }
            PessoaDAO adao = new PessoaDAO();
            Pessoa aToSave = novo ? new Pessoa() : adao.GetById(model.Id);
            Conversor.Converter(model, aToSave, NHibernateBase.Session);
            adao.SaveOrUpdate(aToSave, aToSave.Id);

            if (aToSave.Id > 0)
            {
                return Redirect(string.Format("/Profissionais/Endereco/{0}", aToSave.Id));
            }
            else
            {
                return RedirectToAction("Index", "Profissionais");
            }
        }

        [Role(Roles = "Administrador")]
        public ActionResult Endereco(int id = 0)
        {
            Pessoa p = new PessoaDAO().GetById(id);
            ViewBag.PessoaNome = (p == null) ? "" : p.Nome;
            if (p == null)
            {
                return HttpNotFound();
            }
            PessoaEnderecoVO model = new PessoaEnderecoDAO().GetPessoaEnderecoVOById(id);
            if (model == null)
            {
                model = new PessoaEnderecoVO();
                model.PessoaId = p.Id;
            }
            if (model == null)
            {
                return HttpNotFound();
            }
            EnviarViewBagEndereco();
            return View(model);
        }

        private void EnviarViewBagEndereco()
        {
            ViewBag.ListaUF = ComboBuilder.ListaUF();
            ViewBag.ListaCidade = ComboBuilder.ListaCidade();
            ViewBag.ListaZona = ComboBuilder.ListaZona();
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
            if (!ModelState.IsValid)
            {
                Pessoa pessoa = new PessoaDAO().GetById(model.PessoaId);
                ViewBag.PessoaNome = (pessoa == null) ? "" : pessoa.Nome;
                EnviarViewBagEndereco(); 
                return View(model);
            }
            model.CEP = model.CEP.Replace("-", "");
            Conversor.Converter(model, aToSave, NHibernateBase.Session);
            if (novo)
            {
                aToSave.Pessoa = new PessoaDAO().GetById(model.Id);
            }
            dao.SaveOrUpdate(aToSave, aToSave.Id);

            if (aToSave.Id > 0)
            {
                return Redirect(string.Format("/Profissionais/Documentacao/{0}", aToSave.Id));
            }
            else
            {
                return RedirectToAction("Index", "Profissionais");
            }
        }

        [Role(Roles = "Administrador")]
        public ActionResult Documentacao(int id = 0)
        {
            Pessoa p = new PessoaDAO().GetById(id);
            ViewBag.PessoaNome = (p == null) ? "" : p.Nome;
            if (p == null)
            {
                return HttpNotFound();
            }
            PessoaDocumentacaoVO model = new PessoaDocumentacaoDAO().GetPessoaDocumentacaoVOById(id);
            if (model == null)
            {
                model = new PessoaDocumentacaoVO();
                model.PessoaId = p.Id;
            }
            if (model == null)
            {
                return HttpNotFound();
            }
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
                Pessoa pessoa = new PessoaDAO().GetById(model.PessoaId);
                ViewBag.PessoaNome = (pessoa == null) ? "" : pessoa.Nome;
                EnviarViewBagDocumentacao(); 
                return View(model);
            }
            model.CPFNumero = model.CPFNumero.Replace(".", "");
            model.CPFNumero = model.CPFNumero.Replace("-", "");
            Conversor.Converter(model, aToSave, NHibernateBase.Session);
            if (novo)
            {
                aToSave.Pessoa = new PessoaDAO().GetById(model.PessoaId);
            }
            dao.SaveOrUpdate(aToSave, aToSave.Id);

            if (aToSave.Id > 0)
            {
                return Redirect(string.Format("/Profissionais/Disciplinas/{0}", aToSave.Id));
            }
            else
            {
                return RedirectToAction("Index", "Profissionais");
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

        [Role(Roles = "Administrador")]
        public ActionResult Disciplinas(int id = 0)
        {
            Boolean novo = (id == 0);

            PessoaDAO edao = new PessoaDAO();
            Pessoa p = edao.GetById(id);
            ViewBag.PessoaNome = (p == null) ? "" : p.Nome;
            if (p == null)
            {
                return HttpNotFound();
            }
            PessoaDisciplinaVO model = edao.GetPessoaDisciplina(id);

            // Popula a seção com as disciplinas
            IEnumerable<Disciplina> disciplinas = new DisciplinaDAO().GetListagem();
            Session["ListaItens"] = disciplinas;

            return View(model);
        }

        [HttpPost, ActionName("Disciplinas")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult DisciplinasConfirmed(PessoaDisciplinaVO model)
        {
            PessoaDAO adao = new PessoaDAO();
            adao.GravarDisciplinas(model);
            return RedirectToAction("Index");
        }


    }
}

