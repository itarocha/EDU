using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class DisciplinaVO
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
        
        [Display(Name = "Disciplina Educacenso")]
        [Required(ErrorMessage = "O campo Disciplina do Educacenso deve ser preenchido.")]
        [ConverterEntidade(NomeEntidade="DisciplinaEducacenso", Campo="DisciplinaEducacenso")]
        public virtual int DisciplinaEducacensoId { get; set; }

		public virtual string DisciplinaEducacensoDescricao { get; set; }

        public virtual short ValorEducacenso { get; set; }
        
        [Display(Name = "Ativo?")]
        [Required(ErrorMessage = "O campo Ativo? deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagAtivo { get; set; }
    }
}

