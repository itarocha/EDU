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
using Petra.DAO.Util;
using Visao360.Educacao.Models;

namespace Visao360.Educacao.Controllers
{
    public class HorarioPeriodosController : BaseController
    {
        [Role(Roles = "Administrador")]
        public ActionResult Delete(int Id)
        {
            HorarioPeriodoDAO dao = new HorarioPeriodoDAO();
            HorarioPeriodo model = dao.GetById(Id);

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
            HorarioPeriodoDAO dao = new HorarioPeriodoDAO();
            if (ModelState.IsValid)
            {
                HorarioPeriodo o = dao.GetById(id);
                string inicio = "horaini"; //o.HoraInicio;
                string termino = "horafim";  //o.HoraTermino;

                dao.Delete(o);

                this.FlashMessage(string.Format("Período \"{0}\"-\"{1}\" excluído com sucesso", inicio, termino));
                return RedirectToAction("Index");
            }
            HorarioPeriodo model = dao.GetById(id);
            return View(model);
        }
    }
}

