using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class EscolaEnderecoMap : ClassMap<EscolaEndereco>
    {
        public EscolaEnderecoMap()
        {
            Table("EDU_ESCOLA_ENDERECO");
            Id(x => x.Id).GeneratedBy.Foreign("Escola");
            HasOne(x => x.Escola).Constrained();

            Map(x => x.Logradouro).Column("DS_LOGRADOURO").Length(64);
            Map(x => x.Numero).Column("VL_NUMERO");
            Map(x => x.Complemento).Column("DS_COMPLEMENTO").Length(64);
            Map(x => x.Bairro).Column("DS_BAIRRO").Length(32);
            Map(x => x.CEP).Column("DS_CEP").Length(8);

            References(x => x.Cidade).Column("ID_MUNICIPIO");
            References(x => x.UF).Column("ID_ESTADO");
            References(x => x.Zona).Column("ID_ZONA");

            Map(x => x.Telefone).Column("DS_TELEFONE").Length(16);
            Map(x => x.Fax).Column("DS_FAX").Length(16);
            Map(x => x.Email).Column("DS_EMAIL").Length(64);

            Map(x => x.Latitude).Column("VL_LATITUDE");
            Map(x => x.Longitude).Column("VL_LONGITUDE");
        }
    }
}
