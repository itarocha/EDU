﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Visao360.Educacao.Helpers
{
    public static class PaginadorHelper
    {
        public static MvcHtmlString TextoDestaque(string texto)
        {
            string txt =  String.Format("<h2>Você digitou \"{0}\"</h2>", texto);
            return new MvcHtmlString(txt);

            //TagBuilder txt = new TagBuilder("h1");
            //txt.InnerHtml = String.Format("{0}",texto);
            //return txt.ToString();
        }

        public static MvcHtmlString Paginador(string id)
        {
            string txt = String.Format("<div id=\"{0}\" class=\"pager\">\n", id)+
            "<form>\n" +
                "<span>\n" +
                "Exibir <select class=\"pagesize\">\n" +
                "<option selected=\"selected\"  value=\"10\">10</option>\n" +
                "<option value=\"20\">20</option>\n" +
                "<option value=\"30\">30</option>\n" +
                "<option  value=\"40\">40</option>\n" +
                "</select> registros\n" +
                "</span>\n" +
			    "<img src=\"\\Imagens\\Botoes\\first.png\" class=\"first\"/>\n"+
                "<img src=\"\\Imagens\\Botoes\\prev.png\" class=\"prev\"/>\n" +
                "<input type=\"text\" class=\"pagedisplay\"/>\n" +
                "<img src=\"\\Imagens\\Botoes\\next.png\" class=\"next\"/>\n" +
                "<img src=\"\\Imagens\\Botoes\\last.png\" class=\"last\"/>\n" +
                "</form>\n" +
            "</div>";
            return new MvcHtmlString(txt);
        }


        public static System.Web.WebPages.HelperResult PaginadorFuncionou(string texto)
        {
            return new System.Web.WebPages.HelperResult(__razor_helper_writer =>
            {
                WebViewPage.WriteLiteralTo(@__razor_helper_writer, "<h1>");
                WebViewPage.WriteTo(@__razor_helper_writer, texto);
                WebViewPage.WriteLiteralTo(@__razor_helper_writer, "</h1>");
            });


            //TagBuilder txt = new TagBuilder("h1");
            //txt.InnerHtml = String.Format("{0}",texto);
            //return txt.ToString();
        }
    }
}