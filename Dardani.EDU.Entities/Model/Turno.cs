﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    public class Turno
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Descrição precisa ser preenchida.")]
        [StringLength(64, MinimumLength = 3)]
        [Display(Name = "Descrição")]
        public virtual string Descricao { get; set; }
    }
}
