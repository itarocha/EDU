using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class EscolaPrivadaVO
    {
        public virtual int Id { get; set; }

        public virtual int EscolaId { get; set; }

        [Display(Name = "Categoria Escola Privada")]
        [Required(ErrorMessage = "O campo Categoria Escola Privada deve ser preenchido.")]
        public virtual int CategoriaPrivadaId { get; set; }

        public virtual int CategoriaPrivadaDescricao { get; set; }

        [Display(Name = "Convênio com Poder Público")]
        [Required(ErrorMessage = "O campo Convênio com Poder Público deve ser preenchido.")]
        public virtual int ConvenioPublicoId { get; set; }

        public virtual string ConvenioPublicoDescricao { get; set; }

        [Display(Name = "CNPJ")]
        [ConverterEntidade]
        public virtual string CNPJ { get; set; }

        [Display(Name = "CNPJ da Principal Mantenedora")]
        [ConverterEntidade]
        public virtual string CNPJMantenedora { get; set; }
    }
}
