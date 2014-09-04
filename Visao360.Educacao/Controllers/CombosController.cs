using Dardani.EDU.BO.App;
using Petra.Util.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Visao360.Educacao.Controllers
{
    public class CombosController : Controller
    {
        public ActionResult ListaModalidade()
        {
            IEnumerable<ItemVO> lista = ItemVOBuilders.Instance.BuildListaModalidade();
            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(new SelectList(lista, "Id", "Descricao"), JsonRequestBehavior.AllowGet);
            }
            return View(lista);
        }
        
        public ActionResult ListaTipoAtendimento()
        {
            IEnumerable<ItemVO> lista = ItemVOBuilders.Instance.BuildListaTipoAtendimento();
            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(new SelectList(lista, "Id", "Descricao"), JsonRequestBehavior.AllowGet);
            }
            return View(lista);
        }

    }
}
