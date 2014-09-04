using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class HorarioMap : ClassMap<Horario>
    {
        public HorarioMap()
        {
            Table("EDU_HORARIO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_HORARIO");
            Map(x => x.Descricao).Column("DS_HORARIO").Length(64).Not.Nullable();
            Map(x => x.HoraInicial).Column("DS_HORA_INI").Length(8);
            Map(x => x.HoraFinal).Column("DS_HORA_FIM").Length(8);
        }
    }
}

/*
ALTER TABLE `edu_turma` ADD `ID_HORARIO` int(11) NOT NULL;
ALTER TABLE `edu_turma` ADD KEY `ID_HORARIO` (`ID_HORARIO`); 
INSERT INTO `edu`.`edu_horario` (`DS_HORARIO`, `DS_HORA_INI`, `DS_HORA_FIM`) VALUES ('MATUTINO 50MIN 4H', '07:00', '11:00');
ALTER TABLE `edu_turma` ADD CONSTRAINT `FK7542FABAAE255BCD` FOREIGN KEY (`ID_HORARIO`) REFERENCES `edu_horario` (`ID_HORARIO`);
UPDATE edu_turma tb set tb.ID_HORARIO = 1 WHERE tb.id_turma > 0;
*/

