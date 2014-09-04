using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class EscolaPrivada
    {
        public virtual int Id { get; set; } 

        public virtual Escola Escola { get; set; } 

        [Display(Name = "Categoria Escola Privada")]
        [Required(ErrorMessage = "O campo Categoria Escola Privada deve ser preenchido.")]
        public virtual CategoriaPrivada CategoriaPrivada { get; set; } 

        [Display(Name = "Convênio com Poder Público")]
        [Required(ErrorMessage = "O campo Convênio com Poder Público deve ser preenchido.")]
        public virtual ConvenioPublico ConvenioPublico { get; set; } 

        [Display(Name = "CNPJ")]
        public virtual string CNPJ { get; set; } 

        [Display(Name = "CNPJ da Principal Mantenedora")]
        public virtual string CNPJMantenedora { get; set; } 

    }
}
