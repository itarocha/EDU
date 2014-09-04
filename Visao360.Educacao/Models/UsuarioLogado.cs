using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Visao360.Educacao.Models
{
    public class UsuarioLogado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string Nivel { get; set; }
        public bool IsAdministrador
        {
            get
            {
                return (!String.IsNullOrEmpty(this.Nivel)) && (this.Nivel.ToLower().Equals("administrador"));
            }
        }

        public bool IsVisitante
        {
            get
            {
                return (!String.IsNullOrEmpty(this.Nivel)) && (this.Nivel.ToLower().Equals("visitante"));
            }
        }
    }
}