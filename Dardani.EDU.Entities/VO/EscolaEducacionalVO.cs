﻿using Petra.Util.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.VO
{
    public class EscolaEducacionalVO
    {
        public virtual int Id { get; set; }

        public virtual int EscolaId { get; set; }

        [Display(Name = "Atendimento Educacional Especializado")]
        [Required(ErrorMessage = "O campo Atendimento Educacional Especializado deve ser preenchido.")]
        public virtual int AEEId { get; set; }

        public virtual int AEEDescricao { get; set; }

        [Display(Name = "Atividade Complementar")]
        [Required(ErrorMessage = "O campo Atividade Complementar deve ser preenchido.")]
        public virtual int AtividadeComplementarId { get; set; }

        public virtual int AtividadeComplementarDescricao { get; set; }

        [Display(Name = "Localização Diferenciada")]
        [Required(ErrorMessage = "O campo Localização Diferenciada deve ser preenchido.")]
        public virtual int LocalizacaoDiferenciadaId { get; set; }

        public virtual int LocalizacaoDiferenciadaDescricao { get; set; }

        [Display(Name = "Ensino Fundamental Organizado em Ciclos?")]
        [Required(ErrorMessage = "O campo Ensino Fundamental Organizado em Ciclos deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagEnsinoFundamentalCiclos { get; set; }

        [Display(Name = "Materiais Específicos para Quilombolas?")]
        [Required(ErrorMessage = "O campo Materiais Específicos para Quilombolas deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagMaterialQuilombola { get; set; }

        [Display(Name = "Materiais Específicos para Indígenas?")]
        [Required(ErrorMessage = "O campo Materiais Específicos para Indígenas deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagMaterialIndigena { get; set; }

        [Display(Name = "Educação Indígena?")]
        [Required(ErrorMessage = "O campo Educação Indígena deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagEducacaoIndigena { get; set; }

        [Display(Name = "Ensino Ministrado em Língua Indígena?")]
        [Required(ErrorMessage = "O campo Ensino Ministrado em Língua Indígena deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagEnsinoLinguaIndigena { get; set; }

        [Display(Name = "Ensino Ministrado em Língua Portuguesa?")]
        [Required(ErrorMessage = "O campo Ensino Ministrado em Língua Portuguesa deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagEnsinoLinguaPortuguesa { get; set; }

        [Display(Name = "Língua Indígena")]
        public virtual int LinguaIndigenaId { get; set; }

        public virtual string LinguaIndigenaDescricao { get; set; }

        [Display(Name = "Cede espaço para turmas do Brasil Alfabetizado?")]
        [Required(ErrorMessage = "O campo Brasil Alfabetizado deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagBrasilAlfabetizado { get; set; }

        [Display(Name = "Abre finais de semana para a comunidade?")]
        [Required(ErrorMessage = "O campo Abre Finais de Semana deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagFinalSemana { get; set; }

        [Display(Name = "Possui proposta pedagógica de formação por alternância?")]
        [Required(ErrorMessage = "O campo Formação por Alternância deve ser preenchido.")]
        [StringLength(1, MinimumLength = 1)]
        [ConverterEntidade]
        public virtual string FlagAlternancia { get; set; }

        public virtual int[] ListaModalidades { get; set; }

        public virtual int[] ListaEtapas { get; set; }
    }
}
