﻿using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class TipoAEEMap : ClassMap<TipoAEE>
    {
        public TipoAEEMap()
        {
            Table("EDU_TIPO_AEE");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_TIPO_AEE");
            Map(x => x.Descricao).Column("DS_DESCRICAO").Length(128).Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}
