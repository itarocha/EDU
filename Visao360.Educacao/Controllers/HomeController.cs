using Dardani.EDU.BO.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visao360.Educacao.Filters;
using Visao360.Educacao.Helpers;
using Visao360.Educacao.Models;

namespace Visao360.Educacao.Controllers
{
    public class HomeController : BaseController
    {

        //[Role(Roles = "Administrador")]
        public ActionResult Index(string searchString)
        {
            //IEnumerable<FalecidoEnderecoVO> lista = new List<FalecidoEnderecoVO>();
            //lista = FalecidosService.Instance.ShowLocalizacaoFalecido(db, searchString);
            /*
            IEnumerable<FalecidoEnderecoVO> lista = new FalecidoDAO().ShowLocalizacaoFalecido(searchString);            

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Falecidos", lista);
            }

            return View(lista);
             */
            return View();
        }

        [Role(Roles = "Administrador")]
        public ActionResult Selecionar()
        {
            EscolaSessao e = new EscolaSessao();
            
            //Escola e = GerenciadorEscolaSessao.GetEscolaAtual();

            Session["ListaEscola"] = ItemVOBuilders.Instance.BuildListaEscola();
            Session["ListaAnoLetivo"] = ItemVOBuilders.Instance.BuildListaAnoLetivo();

            //ViewBag.Acao = novo ? "Nova Turma" : "Editar Turma";
            return View(e);
        }

        [HttpPost, ActionName("Selecionar")]
        [Role(Roles = "Administrador")]
        public ActionResult SelecionarConfirmed(EscolaSessao model)
        {
            // Se não é válido, retorna
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            EscolaSessao retorno = GerenciadorEscolaSessao.SetEscola(model);
            //@TempData["mensagem"] = string.Format("Escola {0} é atual escola de trabalho. Ano Letivo = {1}", retorno.EscolaNome, retorno.AnoLetivoAno);

            // Redireciona
            return RedirectToAction("Index","Turmas");
        }

        public JsonResult GetCores()
        {
            List<HtmlColors> lst = new List<HtmlColors>();
            // Monocromático
            lst.Add(new HtmlColors { Id = 001, Bg = "#ffffff", Fg = "#000" });
            lst.Add(new HtmlColors { Id = 002, Bg = "#cccccc", Fg = "#000" });
            lst.Add(new HtmlColors { Id = 003, Bg = "#c0c0c0", Fg = "#000" });
            lst.Add(new HtmlColors { Id = 004, Bg = "#999999", Fg = "#fff" });
            lst.Add(new HtmlColors { Id = 005, Bg = "#666666", Fg = "#fff" });
            lst.Add(new HtmlColors { Id = 006, Bg = "#333333", Fg = "#fff" });
            lst.Add(new HtmlColors { Id = 007, Bg = "#000000", Fg = "#fff" });
            // Vermelho
            lst.Add(new HtmlColors { Id = 008, Bg = "#ffcccc", Fg = "#000" });
            lst.Add(new HtmlColors { Id = 009, Bg = "#ff6666", Fg = "#000" });
            lst.Add(new HtmlColors { Id = 010, Bg = "#ff0000", Fg = "#000" });
            lst.Add(new HtmlColors { Id = 011, Bg = "#cc0000", Fg = "#fff" });
            lst.Add(new HtmlColors { Id = 012, Bg = "#990000", Fg = "#fff" });
            lst.Add(new HtmlColors { Id = 013, Bg = "#660000", Fg = "#fff" });
            lst.Add(new HtmlColors { Id = 014, Bg = "#330000", Fg = "#fff" });
            // Laranja
            lst.Add(new HtmlColors { Id = 015, Bg = "#ffcc99", Fg = "#000" });
            lst.Add(new HtmlColors { Id = 016, Bg = "#ffcc33", Fg = "#000" });
            lst.Add(new HtmlColors { Id = 017, Bg = "#ff9900", Fg = "#000" });
            lst.Add(new HtmlColors { Id = 018, Bg = "#ff6600", Fg = "#fff" });
            lst.Add(new HtmlColors { Id = 019, Bg = "#cc6600", Fg = "#fff" });
            lst.Add(new HtmlColors { Id = 020, Bg = "#993300", Fg = "#fff" });
            lst.Add(new HtmlColors { Id = 021, Bg = "#663300", Fg = "#fff" });
            // Amarelos
            lst.Add(new HtmlColors { Id = 022, Bg = "#ffffcc", Fg = "#000" });
            lst.Add(new HtmlColors { Id = 023, Bg = "#ffff99", Fg = "#000" });
            lst.Add(new HtmlColors { Id = 024, Bg = "#ffff00", Fg = "#000" });
            lst.Add(new HtmlColors { Id = 025, Bg = "#ffcc00", Fg = "#fff" });
            lst.Add(new HtmlColors { Id = 026, Bg = "#999900", Fg = "#fff" });
            lst.Add(new HtmlColors { Id = 027, Bg = "#666600", Fg = "#fff" });
            lst.Add(new HtmlColors { Id = 028, Bg = "#333300", Fg = "#fff" });
            // Verdes
            lst.Add(new HtmlColors { Id = 029, Bg = "#99ff99", Fg = "#000" });
            lst.Add(new HtmlColors { Id = 030, Bg = "#66ff99", Fg = "#000" });
            lst.Add(new HtmlColors { Id = 031, Bg = "#33ff33", Fg = "#000" });
            lst.Add(new HtmlColors { Id = 032, Bg = "#00cc00", Fg = "#fff" });
            lst.Add(new HtmlColors { Id = 033, Bg = "#009900", Fg = "#fff" });
            lst.Add(new HtmlColors { Id = 034, Bg = "#006600", Fg = "#fff" });
            lst.Add(new HtmlColors { Id = 035, Bg = "#003300", Fg = "#fff" });
            // Azuis
            lst.Add(new HtmlColors { Id = 036, Bg = "#ccffff", Fg = "#000" });
            lst.Add(new HtmlColors { Id = 037, Bg = "#66ffff", Fg = "#000" });
            lst.Add(new HtmlColors { Id = 038, Bg = "#33ccff", Fg = "#000" });
            lst.Add(new HtmlColors { Id = 039, Bg = "#3366ff", Fg = "#fff" });
            lst.Add(new HtmlColors { Id = 040, Bg = "#3333ff", Fg = "#fff" });
            lst.Add(new HtmlColors { Id = 041, Bg = "#000099", Fg = "#fff" });
            lst.Add(new HtmlColors { Id = 042, Bg = "#000066", Fg = "#fff" });
            // Magentas
            lst.Add(new HtmlColors { Id = 043, Bg = "#ffccff", Fg = "#000" });
            lst.Add(new HtmlColors { Id = 044, Bg = "#ff99ff", Fg = "#000" });
            lst.Add(new HtmlColors { Id = 045, Bg = "#cc66cc", Fg = "#000" });
            lst.Add(new HtmlColors { Id = 046, Bg = "#cc33cc", Fg = "#fff" });
            lst.Add(new HtmlColors { Id = 047, Bg = "#993366", Fg = "#fff" });
            lst.Add(new HtmlColors { Id = 048, Bg = "#663366", Fg = "#fff" });
            lst.Add(new HtmlColors { Id = 049, Bg = "#330033", Fg = "#fff" });
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFiguras(){
            List<Figura> lst = new List<Figura>();
            lst.Add(new Figura { Descricao = "glyphicon-asterisk" });
            lst.Add(new Figura { Descricao = "glyphicon-plus" });
            lst.Add(new Figura { Descricao = "glyphicon-euro" });
            lst.Add(new Figura { Descricao = "glyphicon-minus" });
            lst.Add(new Figura { Descricao = "glyphicon-cloud" });
            lst.Add(new Figura { Descricao = "glyphicon-envelope" });
            lst.Add(new Figura { Descricao = "glyphicon-pencil" });
            lst.Add(new Figura { Descricao = "glyphicon-glass" });
            lst.Add(new Figura { Descricao = "glyphicon-music" });
            lst.Add(new Figura { Descricao = "glyphicon-search" });
            lst.Add(new Figura { Descricao = "glyphicon-heart" });
            lst.Add(new Figura { Descricao = "glyphicon-star" });
            lst.Add(new Figura { Descricao = "glyphicon-star-empty" });
            lst.Add(new Figura { Descricao = "glyphicon-user" });
            lst.Add(new Figura { Descricao = "glyphicon-film" });
            lst.Add(new Figura { Descricao = "glyphicon-th-large" });
            lst.Add(new Figura { Descricao = "glyphicon-th" });
            lst.Add(new Figura { Descricao = "glyphicon-th-list" });
            lst.Add(new Figura { Descricao = "glyphicon-ok" });
            lst.Add(new Figura { Descricao = "glyphicon-remove" });
            lst.Add(new Figura { Descricao = "glyphicon-zoom-in" });
            lst.Add(new Figura { Descricao = "glyphicon-zoom-out" });
            lst.Add(new Figura { Descricao = "glyphicon-off" });
            lst.Add(new Figura { Descricao = "glyphicon-signal" });
            lst.Add(new Figura { Descricao = "glyphicon-cog" });
            lst.Add(new Figura { Descricao = "glyphicon-trash" });
            lst.Add(new Figura { Descricao = "glyphicon-home" });
            lst.Add(new Figura { Descricao = "glyphicon-file" });
            lst.Add(new Figura { Descricao = "glyphicon-time" });
            lst.Add(new Figura { Descricao = "glyphicon-road" });
            lst.Add(new Figura { Descricao = "glyphicon-download-alt" });
            lst.Add(new Figura { Descricao = "glyphicon-download" });
            lst.Add(new Figura { Descricao = "glyphicon-upload" });
            lst.Add(new Figura { Descricao = "glyphicon-inbox" });
            lst.Add(new Figura { Descricao = "glyphicon-play-circle" });
            lst.Add(new Figura { Descricao = "glyphicon-repeat" });
            lst.Add(new Figura { Descricao = "glyphicon-refresh" });
            lst.Add(new Figura { Descricao = "glyphicon-list-alt" });
            lst.Add(new Figura { Descricao = "glyphicon-lock" });
            lst.Add(new Figura { Descricao = "glyphicon-flag" });
            lst.Add(new Figura { Descricao = "glyphicon-headphones" });
            lst.Add(new Figura { Descricao = "glyphicon-volume-off" });
            lst.Add(new Figura { Descricao = "glyphicon-volume-down" });
            lst.Add(new Figura { Descricao = "glyphicon-volume-up" });
            lst.Add(new Figura { Descricao = "glyphicon-qrcode" });
            lst.Add(new Figura { Descricao = "glyphicon-barcode" });
            lst.Add(new Figura { Descricao = "glyphicon-tag" });
            lst.Add(new Figura { Descricao = "glyphicon-tags" });
            lst.Add(new Figura { Descricao = "glyphicon-book" });
            lst.Add(new Figura { Descricao = "glyphicon-bookmark" });
            lst.Add(new Figura { Descricao = "glyphicon-print" });
            lst.Add(new Figura { Descricao = "glyphicon-camera" });
            lst.Add(new Figura { Descricao = "glyphicon-font" });
            lst.Add(new Figura { Descricao = "glyphicon-bold" });
            lst.Add(new Figura { Descricao = "glyphicon-italic" });
            lst.Add(new Figura { Descricao = "glyphicon-text-height" });
            lst.Add(new Figura { Descricao = "glyphicon-text-width" });
            lst.Add(new Figura { Descricao = "glyphicon-align-left" });
            lst.Add(new Figura { Descricao = "glyphicon-align-center" });
            lst.Add(new Figura { Descricao = "glyphicon-align-right" });
            lst.Add(new Figura { Descricao = "glyphicon-align-justify" });
            lst.Add(new Figura { Descricao = "glyphicon-list" });
            lst.Add(new Figura { Descricao = "glyphicon-indent-left" });
            lst.Add(new Figura { Descricao = "glyphicon-indent-right" });
            lst.Add(new Figura { Descricao = "glyphicon-facetime-video" });
            lst.Add(new Figura { Descricao = "glyphicon-picture" });
            lst.Add(new Figura { Descricao = "glyphicon-map-marker" });
            lst.Add(new Figura { Descricao = "glyphicon-adjust" });
            lst.Add(new Figura { Descricao = "glyphicon-tint" });
            lst.Add(new Figura { Descricao = "glyphicon-edit" });
            lst.Add(new Figura { Descricao = "glyphicon-share" });
            lst.Add(new Figura { Descricao = "glyphicon-check" });
            lst.Add(new Figura { Descricao = "glyphicon-move" });
            lst.Add(new Figura { Descricao = "glyphicon-step-backward" });
            lst.Add(new Figura { Descricao = "glyphicon-fast-backward" });
            lst.Add(new Figura { Descricao = "glyphicon-backward" });
            lst.Add(new Figura { Descricao = "glyphicon-play" });
            lst.Add(new Figura { Descricao = "glyphicon-pause" });
            lst.Add(new Figura { Descricao = "glyphicon-stop" });
            lst.Add(new Figura { Descricao = "glyphicon-forward" });
            lst.Add(new Figura { Descricao = "glyphicon-fast-forward" });
            lst.Add(new Figura { Descricao = "glyphicon-step-forward" });
            lst.Add(new Figura { Descricao = "glyphicon-eject" });
            lst.Add(new Figura { Descricao = "glyphicon-chevron-left" });
            lst.Add(new Figura { Descricao = "glyphicon-chevron-right" });
            lst.Add(new Figura { Descricao = "glyphicon-plus-sign" });
            lst.Add(new Figura { Descricao = "glyphicon-minus-sign" });
            lst.Add(new Figura { Descricao = "glyphicon-remove-sign" });
            lst.Add(new Figura { Descricao = "glyphicon-ok-sign" });
            lst.Add(new Figura { Descricao = "glyphicon-question-sign" });
            lst.Add(new Figura { Descricao = "glyphicon-info-sign" });
            lst.Add(new Figura { Descricao = "glyphicon-screenshot" });
            lst.Add(new Figura { Descricao = "glyphicon-remove-circle" });
            lst.Add(new Figura { Descricao = "glyphicon-ok-circle" });
            lst.Add(new Figura { Descricao = "glyphicon-ban-circle" });
            lst.Add(new Figura { Descricao = "glyphicon-arrow-left" });
            lst.Add(new Figura { Descricao = "glyphicon-arrow-right" });
            lst.Add(new Figura { Descricao = "glyphicon-arrow-up" });
            lst.Add(new Figura { Descricao = "glyphicon-arrow-down" });
            lst.Add(new Figura { Descricao = "glyphicon-share-alt" });
            lst.Add(new Figura { Descricao = "glyphicon-resize-full" });
            lst.Add(new Figura { Descricao = "glyphicon-resize-small" });
            lst.Add(new Figura { Descricao = "glyphicon-exclamation-sign" });
            lst.Add(new Figura { Descricao = "glyphicon-gift" });
            lst.Add(new Figura { Descricao = "glyphicon-leaf" });
            lst.Add(new Figura { Descricao = "glyphicon-fire" });
            lst.Add(new Figura { Descricao = "glyphicon-eye-open" });
            lst.Add(new Figura { Descricao = "glyphicon-eye-close" });
            lst.Add(new Figura { Descricao = "glyphicon-warning-sign" });
            lst.Add(new Figura { Descricao = "glyphicon-plane" });
            lst.Add(new Figura { Descricao = "glyphicon-calendar" });
            lst.Add(new Figura { Descricao = "glyphicon-random" });
            lst.Add(new Figura { Descricao = "glyphicon-comment" });
            lst.Add(new Figura { Descricao = "glyphicon-magnet" });
            lst.Add(new Figura { Descricao = "glyphicon-chevron-up" });
            lst.Add(new Figura { Descricao = "glyphicon-chevron-down" });
            lst.Add(new Figura { Descricao = "glyphicon-retweet" });
            lst.Add(new Figura { Descricao = "glyphicon-shopping-cart" });
            lst.Add(new Figura { Descricao = "glyphicon-folder-close" });
            lst.Add(new Figura { Descricao = "glyphicon-folder-open" });
            lst.Add(new Figura { Descricao = "glyphicon-resize-vertical" });
            lst.Add(new Figura { Descricao = "glyphicon-resize-horizontal" });
            lst.Add(new Figura { Descricao = "glyphicon-hdd" });
            lst.Add(new Figura { Descricao = "glyphicon-bullhorn" });
            lst.Add(new Figura { Descricao = "glyphicon-bell" });
            lst.Add(new Figura { Descricao = "glyphicon-certificate" });
            lst.Add(new Figura { Descricao = "glyphicon-thumbs-up" });
            lst.Add(new Figura { Descricao = "glyphicon-thumbs-down" });
            lst.Add(new Figura { Descricao = "glyphicon-hand-right" });
            lst.Add(new Figura { Descricao = "glyphicon-hand-left" });
            lst.Add(new Figura { Descricao = "glyphicon-hand-up" });
            lst.Add(new Figura { Descricao = "glyphicon-hand-down" });
            lst.Add(new Figura { Descricao = "glyphicon-circle-arrow-right" });
            lst.Add(new Figura { Descricao = "glyphicon-circle-arrow-left" });
            lst.Add(new Figura { Descricao = "glyphicon-circle-arrow-up" });
            lst.Add(new Figura { Descricao = "glyphicon-circle-arrow-down" });
            lst.Add(new Figura { Descricao = "glyphicon-globe" });
            lst.Add(new Figura { Descricao = "glyphicon-wrench" });
            lst.Add(new Figura { Descricao = "glyphicon-tasks" });
            lst.Add(new Figura { Descricao = "glyphicon-filter" });
            lst.Add(new Figura { Descricao = "glyphicon-briefcase" });
            lst.Add(new Figura { Descricao = "glyphicon-fullscreen" });
            lst.Add(new Figura { Descricao = "glyphicon-dashboard" });
            lst.Add(new Figura { Descricao = "glyphicon-paperclip" });
            lst.Add(new Figura { Descricao = "glyphicon-heart-empty" });
            lst.Add(new Figura { Descricao = "glyphicon-link" });
            lst.Add(new Figura { Descricao = "glyphicon-phone" });
            lst.Add(new Figura { Descricao = "glyphicon-pushpin" });
            lst.Add(new Figura { Descricao = "glyphicon-usd" });
            lst.Add(new Figura { Descricao = "glyphicon-gbp" });
            lst.Add(new Figura { Descricao = "glyphicon-sort" });
            lst.Add(new Figura { Descricao = "glyphicon-sort-by-alphabet" });
            lst.Add(new Figura { Descricao = "glyphicon-sort-by-alphabet-alt" });
            lst.Add(new Figura { Descricao = "glyphicon-sort-by-order" });
            lst.Add(new Figura { Descricao = "glyphicon-sort-by-order-alt" });
            lst.Add(new Figura { Descricao = "glyphicon-sort-by-attributes" });
            lst.Add(new Figura { Descricao = "glyphicon-sort-by-attributes-alt" });
            lst.Add(new Figura { Descricao = "glyphicon-unchecked" });
            lst.Add(new Figura { Descricao = "glyphicon-expand" });
            lst.Add(new Figura { Descricao = "glyphicon-collapse-down" });
            lst.Add(new Figura { Descricao = "glyphicon-collapse-up" });
            lst.Add(new Figura { Descricao = "glyphicon-log-in" });
            lst.Add(new Figura { Descricao = "glyphicon-flash" });
            lst.Add(new Figura { Descricao = "glyphicon-log-out" });
            lst.Add(new Figura { Descricao = "glyphicon-new-window" });
            lst.Add(new Figura { Descricao = "glyphicon-record" });
            lst.Add(new Figura { Descricao = "glyphicon-save" });
            lst.Add(new Figura { Descricao = "glyphicon-open" });
            lst.Add(new Figura { Descricao = "glyphicon-saved" });
            lst.Add(new Figura { Descricao = "glyphicon-import" });
            lst.Add(new Figura { Descricao = "glyphicon-export" });
            lst.Add(new Figura { Descricao = "glyphicon-send" });
            lst.Add(new Figura { Descricao = "glyphicon-floppy-disk" });
            lst.Add(new Figura { Descricao = "glyphicon-floppy-saved" });
            lst.Add(new Figura { Descricao = "glyphicon-floppy-remove" });
            lst.Add(new Figura { Descricao = "glyphicon-floppy-save" });
            lst.Add(new Figura { Descricao = "glyphicon-floppy-open" });
            lst.Add(new Figura { Descricao = "glyphicon-credit-card" });
            lst.Add(new Figura { Descricao = "glyphicon-transfer" });
            lst.Add(new Figura { Descricao = "glyphicon-cutlery" });
            lst.Add(new Figura { Descricao = "glyphicon-header" });
            lst.Add(new Figura { Descricao = "glyphicon-compressed" });
            lst.Add(new Figura { Descricao = "glyphicon-earphone" });
            lst.Add(new Figura { Descricao = "glyphicon-phone-alt" });
            lst.Add(new Figura { Descricao = "glyphicon-tower" });
            lst.Add(new Figura { Descricao = "glyphicon-stats" });
            lst.Add(new Figura { Descricao = "glyphicon-sd-video" });
            lst.Add(new Figura { Descricao = "glyphicon-hd-video" });
            lst.Add(new Figura { Descricao = "glyphicon-subtitles" });
            lst.Add(new Figura { Descricao = "glyphicon-sound-stereo" });
            lst.Add(new Figura { Descricao = "glyphicon-sound-dolby" });
            lst.Add(new Figura { Descricao = "glyphicon-sound-5-1" });
            lst.Add(new Figura { Descricao = "glyphicon-sound-6-1" });
            lst.Add(new Figura { Descricao = "glyphicon-sound-7-1" });
            lst.Add(new Figura { Descricao = "glyphicon-copyright-mark" });
            lst.Add(new Figura { Descricao = "glyphicon-registration-mark" });
            lst.Add(new Figura { Descricao = "glyphicon-cloud-download" });
            lst.Add(new Figura { Descricao = "glyphicon-cloud-upload" });
            lst.Add(new Figura { Descricao = "glyphicon-tree-conifer" });
            lst.Add(new Figura { Descricao = "glyphicon-tree-deciduous" });
            return Json(lst,JsonRequestBehavior.AllowGet);
        }

    }
}
