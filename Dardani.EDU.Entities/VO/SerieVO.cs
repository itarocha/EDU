using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class SerieVO
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Descrição precisa ser preenchida.")]
        [StringLength(64, MinimumLength = 3)]
        [Display(Name = "Descrição")]
        [ConverterEntidade]
        public virtual string Descricao { get; set; }

        [Required(ErrorMessage = "Descrição Abreviada precisa ser preenchida.")]
        [StringLength(8, MinimumLength = 2)]
        [Display(Name = "Descrição Abreviada")]
        [ConverterEntidade]
        public virtual string DescricaoAbreviada { get; set; }

        [Required(ErrorMessage = "Sequência precisa ser preenchida.")]
        [Display(Name = "Sequência")]
        [ConverterEntidade]
        public virtual int Sequencia { get; set; }

        [Display(Name = "Ensino")]
        [Required(ErrorMessage = "Ensino precisa ser informado")]

        //[ConverterEntidade(ClasseDao = "Dardani.EDU.BO.NH.EnsinoDAO", FileName = "Dardani.EDU.BO.dll")]
        public virtual int EnsinoId { get; set; }

        public virtual string EnsinoDescricao { get; set; }

        public virtual string EnsinoDescricaoAbreviada { get; set; }
    }
}
