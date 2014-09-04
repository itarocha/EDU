using Petra.Util.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.BO.App
{
    public static class EDUListasBuilder
    {

        public static IEnumerable<ItemStringVO> BuildListaNiveis()
        {
            List<ItemStringVO> lista = new List<ItemStringVO>();
            lista.Add(new ItemStringVO { Id = "Administrador", Descricao = "Administrador" });
            lista.Add(new ItemStringVO { Id = "Visitante", Descricao = "Visitante" });
            return lista;
        }

        public static IEnumerable<ItemStringVO> BuildListaUF()
        {
            List<ItemStringVO> lista = new List<ItemStringVO>();
            lista.Add(new ItemStringVO { Id = "XX", Descricao = "--Selecione--" });
            lista.Add(new ItemStringVO { Id = "AC", Descricao = "Acre" });
            lista.Add(new ItemStringVO { Id = "AL", Descricao = "Alagoas" });
            lista.Add(new ItemStringVO { Id = "AM", Descricao = "Amazonas" });
            lista.Add(new ItemStringVO { Id = "AP", Descricao = "Amapá" });
            lista.Add(new ItemStringVO { Id = "BA", Descricao = "Bahia" });
            lista.Add(new ItemStringVO { Id = "CE", Descricao = "Ceará" });
            lista.Add(new ItemStringVO { Id = "DF", Descricao = "Distrito Federal" });
            lista.Add(new ItemStringVO { Id = "ES", Descricao = "Espírito Santo" });
            lista.Add(new ItemStringVO { Id = "GO", Descricao = "Goiás" });
            lista.Add(new ItemStringVO { Id = "MA", Descricao = "Maranhão" });
            lista.Add(new ItemStringVO { Id = "MG", Descricao = "Minas Gerais" });
            lista.Add(new ItemStringVO { Id = "MS", Descricao = "Mato Grosso do Sul" });
            lista.Add(new ItemStringVO { Id = "MT", Descricao = "Mato Grosso" });
            lista.Add(new ItemStringVO { Id = "PA", Descricao = "Pará" });
            lista.Add(new ItemStringVO { Id = "PB", Descricao = "Paraíba" });
            lista.Add(new ItemStringVO { Id = "PE", Descricao = "Pernambuco" });
            lista.Add(new ItemStringVO { Id = "PI", Descricao = "Piauí" });
            lista.Add(new ItemStringVO { Id = "PR", Descricao = "Paraná" });
            lista.Add(new ItemStringVO { Id = "RJ", Descricao = "Rio de Janeiro" });
            lista.Add(new ItemStringVO { Id = "RN", Descricao = "Rio Grande do Norte" });
            lista.Add(new ItemStringVO { Id = "RO", Descricao = "Rondônia" });
            lista.Add(new ItemStringVO { Id = "RR", Descricao = "Roraima" });
            lista.Add(new ItemStringVO { Id = "RS", Descricao = "Rio Grande do Sul" });
            lista.Add(new ItemStringVO { Id = "SC", Descricao = "Santa Catarina" });
            lista.Add(new ItemStringVO { Id = "SE", Descricao = "Sergipe" });
            lista.Add(new ItemStringVO { Id = "SP", Descricao = "São Paulo" });
            lista.Add(new ItemStringVO { Id = "TO", Descricao = "Tocantins" });
            //return lista.OrderBy(o => o.Descricao);
            return lista;
        }

        public static IEnumerable<ItemStringVO> BuildListaSimNao()
        {
            List<ItemStringVO> lista = new List<ItemStringVO>();
            lista.Add(new ItemStringVO { Id = "S", Descricao = "SIM" });
            lista.Add(new ItemStringVO { Id = "N", Descricao = "NÃO" });
            return lista;
        }











        public static IEnumerable<ItemStringVO> BuildListaControleFrequencia()
        {
            List<ItemStringVO> lista = new List<ItemStringVO>();
            lista.Add(new ItemStringVO { Id = "P", Descricao = "Períodos" });
            lista.Add(new ItemStringVO { Id = "D", Descricao = "Dias Letivos" });
            return lista;
        }

        public static IEnumerable<ItemStringVO> BuildListaMediaFrequencia()
        {
            List<ItemStringVO> lista = new List<ItemStringVO>();
            lista.Add(new ItemStringVO { Id = "G", Descricao = "Global" });
            lista.Add(new ItemStringVO { Id = "I", Descricao = "Individual" });
            return lista;
        }

        public static IEnumerable<ItemStringVO> BuildListaDisciplinaCategoria()
        {
            List<ItemStringVO> lista = new List<ItemStringVO>();
            lista.Add(new ItemStringVO { Id = "N", Descricao = "Base Nacional Comum" });
            lista.Add(new ItemStringVO { Id = "D", Descricao = "Parte Diversificada" });
            return lista;
        }

        public static IEnumerable<ItemStringVO> BuildListaTipoAvaliacao()
        {
            List<ItemStringVO> lista = new List<ItemStringVO>();
            lista.Add(new ItemStringVO { Id = "N", Descricao = "Nota" });
            lista.Add(new ItemStringVO { Id = "C", Descricao = "Conceito" });
            return lista;
        }

    }
}
