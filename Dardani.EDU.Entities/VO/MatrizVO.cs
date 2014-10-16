using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class MatrizVO
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Ano Letivo precisa ser informado.")]
        [Display(Name = "Ano Letivo")]
        [ConverterEntidade(NomeEntidade = "AnoLetivo", Campo = "AnoLetivo")]
        public virtual int AnoLetivoId { get; set; }

        [Display(Name = "Modalidade")]
        public virtual int ModalidadeId { get; set; }

        [Required(ErrorMessage = "Etapa precisa ser informada.")]
        [Display(Name = "Etapa")]
        [ConverterEntidade(NomeEntidade = "Etapa", Campo = "Etapa")]
        public virtual int EtapaId { get; set; }

        [Display(Name = "Descrição")]
        [ConverterEntidade]
        public virtual string Descricao { get; set; }

        [Required(ErrorMessage = "Dias Letivos precisa ser preenchido.")]
        [Display(Name = "Dias Letivos")]
        [ConverterEntidade]
        public virtual short DiasLetivos { get; set; } // 200

        [Required(ErrorMessage = "Carga Horária Semanal precisa ser preenchido.")]
        [Display(Name = "Carga Horária Semanal")]
        [ConverterEntidade]
        public virtual short CargaHorariaSemanal { get; set; } // 20 = 5 dias x 4 aulas dia

        [Required(ErrorMessage = "Carga Horária precisa ser preenchido.")]
        [Display(Name = "Carga Horária (minutos)")]
        [ConverterEntidade]
        public virtual short CargaHorariaAula { get; set; } // 50 minutos cada aula

        [Required(ErrorMessage = "Número de Semanas Letivas precisa ser preenchido.")]
        [Display(Name = "Número de Semanas")]
        [ConverterEntidade]
        public virtual short NumeroSemanasLetivas { get; set; } // 40 semanas letivas
        
        public IEnumerable<MatrizDisciplinaVO> Disciplinas {get; set;}
    }
}
