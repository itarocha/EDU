﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class PessoaDisciplina
    {
        public virtual int Id { get; set; }

        [Display(Name = "Profissional")]
        [Required(ErrorMessage = "O campo Profissional deve ser preenchido.")]
        public virtual Pessoa Pessoa { get; set; }

        [Display(Name = "Disciplina")]
        [Required(ErrorMessage = "O campo Disciplina deve ser preenchido.")]
        public virtual Disciplina Disciplina { get; set; }

    }
}
