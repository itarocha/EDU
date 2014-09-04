using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class EscolaComputadorVO
    {
        public virtual int Id { get; set; }

        public virtual int EscolaId { get; set; }

        [Display(Name = "Qtd. Computadores para Uso Administrativo")]
        [ConverterEntidade]
        public virtual short QuantidadeUsoAdministrativo { get; set; }

        [Display(Name = "Qtd. Computadores para Uso dos Alunos")]
        [ConverterEntidade]
        public virtual short QuantidadeUsoAluno { get; set; }

        [Display(Name = "Possui Acesso a Internet?")]
        [ConverterEntidade]
        public virtual string FlagAcessoInternet { get; set; }

        [Display(Name = "Internet com Banda Larga?")]
        [ConverterEntidade]
        public virtual string FlagBandaLarga { get; set; } 

    }
}
