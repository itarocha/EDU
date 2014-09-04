using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Model
{
    //[Table("CMT_ESTADO_CIVIL")]
    public class EstadoCivil
    {
        //[Key, Column("ID_ESTADO_CIVIL")]
        public virtual int Id { get; set; }

        //[Column("DS_ESTADO_CIVIL"), MaxLength(64)]
        [Required(ErrorMessage = "Descrição precisa ser preenchida.")]
        [StringLength(64, MinimumLength = 3)]
        [Display(Name = "Descricao")]
        public virtual string Descricao { get; set; }

        [Display(Name = "Valor no Educacenso")]
        [Required(ErrorMessage = "O campo Valor no Educacenso deve ser preenchido.")]
        public virtual short ValorEducacenso { get; set; } 
    }
}
