using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class SalaVO
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Descrição precisa ser preenchida.")]
        [StringLength(32, MinimumLength = 1)]
        [Display(Name = "Descrição")]
        [ConverterEntidade]
        public virtual string Descricao { get; set; }

        [Required(ErrorMessage = "Metragem precisa ser preenchida.")]
        [Range(typeof(Decimal), "1", "9999", ErrorMessage = "Campo deve ser numérico")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} precisa ser numérico.")]
        [ConverterEntidade]
        public virtual decimal Metragem { get; set; }

        [Required(ErrorMessage = "Capacidade precisa ser preenchida.")]
        [ConverterEntidade]
        public virtual int Capacidade { get; set; }

        [Display(Name = "Escola")]
        [Required(ErrorMessage = "Escola precisa ser informada")]
        [ConverterEntidade(NomeEntidade="Escola",Campo="Escola")]
        public virtual int EscolaId { get; set; }

        [Display(Name = "Nome da Escola")]
        public virtual string EscolaNome { get; set; }

        [Display(Name = "Tipo de Sala")]
        [Required(ErrorMessage = "Tipo de Sala precisa ser informado")]
        [ConverterEntidade(NomeEntidade = "TipoSala", Campo = "TipoSala")]
        public virtual int TipoSalaId { get; set; }

        [Display(Name = "Tipo de Sala")]
        public virtual string TipoSalaDescricao { get; set; }

        [Display(Name = "Sala de Aula?")]
        public virtual string TipoSalaFlagSalaAula { get; set; }
    }
}
