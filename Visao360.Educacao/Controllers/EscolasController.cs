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
using Petra.DAO.Util;
using Visao360.Educacao.Models;
using System.Globalization;

namespace Visao360.Educacao.Controllers
{
    public class EscolasController : BaseController
    {
        [Role(Roles = "Administrador")]
        public ActionResult Index(string searchString)
        {
            IEnumerable<Escola> lista = new EscolaDAO().GetListagem(searchString);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Listagem", lista);
            }
            return View(lista);
        }

        [Role(Roles = "Administrador")]
        public ActionResult TornarPadrao(int id = 0)
        {
            Boolean novo = (id == 0);
            EscolaVO model = null;
            EscolaDAO dao = new EscolaDAO();

            model = dao.GetEscolaVOById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            //GerenciadorEscolaSessao.SetEscola(id);
            //@TempData["mensagem"] = string.Format("Escola {0} é atual escola de trabalho", model.Nome);

            return RedirectToAction("Index");
        }

        private void EnviarViewBagEdit() {
            ViewBag.ListaSituacaoFuncionamento = ComboBuilder.ListaSituacaoFuncionamento();
            ViewBag.ListaTipoAdministracao = ComboBuilder.ListaTipoAdministracao();
            ViewBag.ListaEstagioRegulamentacao = ComboBuilder.ListaEstagioRegulamentacao();
            ViewBag.ListaSimNao = ComboBuilder.ListaSimNao();
        }

        [Role(Roles = "Administrador")]
        public ActionResult Edit(int id = 0)
        {
            Boolean novo = (id == 0);
            EscolaVO model = null;
            EscolaDAO dao = new EscolaDAO();

            if (novo)
            {
                model = new EscolaVO();
            }
            else
            {
                model = dao.GetEscolaVOById(id);
                if (model == null)
                {
                    return HttpNotFound();
                }
                GerenciadorEscolaSessao.SetEscola(new EscolaSessao() { EscolaId = id });
            }
            EnviarViewBagEdit();
            ViewBag.Acao = novo ? "Nova Escola" : "Editar Escola";
            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult EditConfirmed(EscolaVO model)
        {
            // Verifica se é novo
            Boolean novo = (model.Id == 0);

            // Se não é válido, retorna
            if (!ModelState.IsValid)
            {
                ViewBag.Acao = novo ? "Nova Escola" : "Editar Escola";
                EnviarViewBagEdit();
                return View(model);
            }

            // Pega o repositório
            EscolaDAO adao = new EscolaDAO();
            Escola aToSave = novo ? new Escola() : adao.GetById(model.Id);

            // Converte VO para Entity
            Conversor.Converter(model, aToSave, NHibernateBase.Session);

            // Grava
            adao.SaveOrUpdate(aToSave, aToSave.Id);
             
            // Redireciona
            return RedirectToAction("Endereco", new { id = aToSave.Id });
        }

        [Role(Roles = "Administrador")]
        public ActionResult Endereco(int id = 0)
        {
            Boolean novo = (id == 0);

            Escola p = new EscolaDAO().GetById(id);

            EscolaEnderecoVO model = new EscolaEnderecoDAO().GetEscolaEnderecoVOById(id);
            if (model == null)
            {
                model = new EscolaEnderecoVO();
                model.EscolaId = p.Id;
            }

            if (model == null)
            {
                return HttpNotFound();
            }
            // Popula a seção com os combos
            GerenciadorEscolaSessao.SetEscola(new EscolaSessao() { EscolaId = id });
            EnviarViewBagEndereco();
            return View(model);
        }

        private void EnviarViewBagEndereco(){
            ViewBag.ListaUF = ComboBuilder.ListaUF();
            ViewBag.ListaCidade = ComboBuilder.ListaCidade();
            ViewBag.ListaZona = ComboBuilder.ListaZona();
        }

        [HttpPost, ActionName("Endereco")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult EnderecoConfirmed(EscolaEnderecoVO model)
        {
            // Verifica se é novo
            EscolaEnderecoDAO adao = new EscolaEnderecoDAO();
            EscolaEndereco aToSave = adao.GetById(model.Id);

            Boolean novo = (aToSave == null);
            if (novo)
            {
                aToSave = new EscolaEndereco();
            }

            model.CEP = model.CEP != null ? model.CEP.Replace("-", "") : null;
            ModelState.Remove("CEP");
            ModelState.SetModelValue("CEP", new ValueProviderResult(model.CEP, String.Empty, CultureInfo.InvariantCulture));

            // Se não é válido, retorna
            if (!ModelState.IsValid)
            {
                EnviarViewBagEndereco();
                return View(model);
            }

            // Pega o repositório
            //EscolaEndereco aToSave = novo ? new EscolaEndereco() : adao.GetById(model.Id);

            // Converte VO para Entity
            Conversor.Converter(model, aToSave, NHibernateBase.Session);

            // Seta os objetos
            aToSave.Zona = new ZonaDAO().GetById(model.ZonaId);
            if (novo)
            {
                aToSave.Escola = new EscolaDAO().GetById(model.EscolaId);
            }

            // Grava
            adao.SaveOrUpdate(aToSave, aToSave.Id);

            // Redireciona
            return RedirectToAction("Infraestrutura", new { id = aToSave.Id });
        }

        [Role(Roles = "Administrador")]
        public ActionResult Infraestrutura(int id = 0)
        {
            Boolean novo = (id == 0);

            EscolaDAO edao = new EscolaDAO();
            Escola p = edao.GetById(id);
            // if (p == null) HTTPNOTFOUND


            //EscolaEnderecoVO model = new EscolaEnderecoDAO().GetEscolaEnderecoVOById(id);
            /*
            if (model == null)
            {
                model = new EscolaEnderecoVO();
                model.EscolaId = p.Id;
            }
            */

            if (p == null)
            {
                return HttpNotFound();
            }
            GerenciadorEscolaSessao.SetEscola(new EscolaSessao() { EscolaId = id });

            EscolaInfraestruturaVO model = edao.GetEscolaInfraestrutura(id);

            IEnumerable<InfraestruturaCategoria> categorias = new InfraestruturaCategoriaDAO().GetListagemFull();
            ViewBag.ListaItens = categorias;

            return View(model);
        }

        [HttpPost, ActionName("Infraestrutura")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult InfraestruturaConfirmed(EscolaInfraestruturaVO model)
        {
            // Verifica se é novo
            EscolaDAO adao = new EscolaDAO();

            // Grava
            adao.GravarInfraestrutura(model);

            // Redireciona
            return RedirectToAction("DadosEducacionais", new { id = model.EscolaId });
        }

        [Role(Roles = "Administrador")]
        public ActionResult DadosEducacionais(int id = 0)
        {
            Boolean novo = (id == 0);

            EscolaDAO edao = new EscolaDAO();
            Escola p = edao.GetById(id);
            // if (p == null) HTTPNOTFOUND


            //EscolaEnderecoVO model = new EscolaEnderecoDAO().GetEscolaEnderecoVOById(id);
            /*
            if (model == null)
            {
                model = new EscolaEnderecoVO();
                model.EscolaId = p.Id;
            }
            */

            if (p == null)
            {
                return HttpNotFound();
            }

            EscolaEducacionalVO model = edao.GetEscolaEducacional(id);

            EnviarViewBagDadosEducacionais();
            // Popula a seção com as modalidades

            return View(model);
        }

        private void EnviarViewBagDadosEducacionais()
        {
            ViewBag.ListaSimNao = ComboBuilder.ListaSimNao();
            ViewBag.ListaAEE = ComboBuilder.ListaAEE();
            ViewBag.ListaAtividadeComplementar = ComboBuilder.ListaAtividadeComplementar();
            ViewBag.ListaLocalizacaoDiferenciada = ComboBuilder.ListaLocalizacaoDiferenciada();
            ViewBag.ListaLinguaIndigena = ComboBuilder.ListaLinguaIndigena();

            IEnumerable<Modalidade> modalidades = new ModalidadeDAO().GetListagemFull();
            ViewBag.ListaModalidade = modalidades;
        }


        [HttpPost, ActionName("DadosEducacionais")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult DadosEducacionaisConfirmed(EscolaEducacionalVO model)
        {
            // Verifica se é novo
            
            EscolaDAO dao = new EscolaDAO();

            // Grava
            /*
            dao.GravarEducacional(model);
            */
            // Redireciona
            return RedirectToAction("Index");
        }

        [Role(Roles = "Administrador")]
        public ActionResult ListagemSalasJson()/*(int escolaId = 1)*/
        {
            int escolaId = 1;

            IEnumerable<Sala> lista = new SalaDAO().GetListagemByEscolaId(escolaId);
            /*
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListagemSalas", lista);
            }
            return View(lista);
            */
            var retorno = Json(lista, JsonRequestBehavior.AllowGet);

            return retorno;
        }

        //  SALAS DE AULA DA ESCOLA
        [Role(Roles = "Administrador")]
        public ActionResult SalaEdit(int id = 0)
        {
            Boolean novo = (id == 0);
            SalaVO model = null;
            SalaDAO dao = new SalaDAO();

            if (novo)
            {
                model = new SalaVO();
            }
            else
            {
                model = dao.GetVOById(id);
                if (model == null)
                {
                    return HttpNotFound();
                }
            }
            return View(model);
        }

        [HttpPost, ActionName("SalaEdit")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult SalaEditConfirmed(SalaVO model)
        {
            Boolean novo = (model.Id == 0);

            EscolaDAO edao = new EscolaDAO();
            TipoSalaDAO tsdao = new TipoSalaDAO();

            SalaDAO dao = new SalaDAO();
            Sala toSave = novo ? new Sala() : dao.GetById(model.Id);
            Conversor.Converter(model, toSave);

            toSave.Escola = edao.GetById(model.EscolaId);
            toSave.TipoSala = tsdao.GetById(model.TipoSalaId);

            if (toSave.Escola == null)
            {
                ModelState.AddModelError("EscolaId", "Escola deve ser preenchida");
            }

            if (toSave.TipoSala == null)
            {
                ModelState.AddModelError("TipoSalaId", "Tipo de Sala deve ser preenchida");
            }

            if ((!ModelState.IsValid))
            {
                //ViewBag.Acao = novo ? "Novo Ensino" : "Editar Ensino";

                return PartialView("_SalaEdit", model);
            }

            dao.SaveOrUpdate(toSave, toSave.Id);

            return PartialView("_SalaEdit", model);
        }

        [Role(Roles = "Administrador")]
        public ActionResult Delete(int Id)
        {
            EscolaDAO dao = new EscolaDAO();
            Escola model = dao.GetById(Id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult DeleteConfirmed(int id)
        {
            /*
            bool existe = new LoteDAO().ExisteLotePorEnsinoId(id);

            if (existe)
            {
                ModelState.AddModelError("Id", "Existem Lotes para esse Tipo de Lote. Exclusão não permitida.");
            }
            */
            EscolaDAO dao = new EscolaDAO();
            if (ModelState.IsValid)
            {
                Escola o = dao.GetById(id);
                string descricao = o.Nome;
                dao.Delete(o);

                this.FlashMessage(string.Format("Escola \"{0}\" excluída com sucesso", descricao));
                return RedirectToAction("Index");
            }
            Escola model = dao.GetById(id);
            return View(model);
        }
    }
}

