using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class EscolaComputador
    {
        public virtual int Id { get; set; } 

        public virtual Escola Escola { get; set; } 

        [Display(Name = "Qtd. Computadores para Uso Administrativo")]
        public virtual short QuantidadeUsoAdministrativo { get; set; } 

        [Display(Name = "Qtd. Computadores para Uso dos Alunos")]
        public virtual short QuantidadeUsoAluno { get; set; } 

        [Display(Name = "Possui Acesso a Internet?")]
        public virtual string FlagAcessoInternet { get; set; } 

        [Display(Name = "Internet com Banda Larga?")]
        public virtual string FlagBandaLarga { get; set; } 

    }
}
