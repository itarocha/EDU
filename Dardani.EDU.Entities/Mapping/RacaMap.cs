﻿using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class RacaMap : ClassMap<Raca>
    {
        public RacaMap()
        {
            Table("EDU_RACA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_RACA");
            Map(x => x.Descricao).Column("DS_RACA").Length(64).Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
        }
    }
}
