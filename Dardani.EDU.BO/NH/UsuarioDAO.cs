using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Transform;
using NHibernate.Criterion;
using Dardani.EDU.Entities.Model;
using Dardani.EDU.Entities.VO;
using Petra.DAO.NH;

namespace Dardani.EDU.BO.NH
{
    // Talvez seja interesante...
    // Definir perfil de Usuário: Diretor, Secretário, Profissional, Aluno...  independente do tipo de acesso
    // Dependendo do perfil, vai direcionar para uma página ou outra.
    public class UsuarioDAO : GenericDAO<Usuario>
    {
        public bool UsuarioPossuiPermissao(string nome, string acaoId) {
            UsuarioAcao ua = Session.CreateQuery("SELECT ua "+
                "FROM UsuarioAcao ua "+ 
                "WHERE ua.Acao.Id = :acaoId  "+
                "AND ua.Usuario.NomeUsuario = :nome "+
                "AND ua.Acao.FlagAtivo = :flagAtivo")
                .SetParameter("acaoId", acaoId)
                .SetParameter("nome", nome)
                .SetParameter("flagAtivo", "S")
                .UniqueResult<UsuarioAcao>();

            return (ua != null);
        }

        /*
        public string GetNivelByUsuario(string nome)
        {
            String model = Session.CreateQuery(
                "SELECT u.Nivel " +
                "FROM Usuario u " +
                "WHERE u.NomeUsuario = :nomeUsuario ")
                .SetParameter("nomeUsuario", nome)
                .SetResultTransformer(Transformers.AliasToBean(typeof(String)))
                .UniqueResult<String>();

            return model;
        }
        */

        public UsuarioVO GetUsuarioByNomeSenha(string nomeUsuario, string senhaAcesso)
        {
            UsuarioVO vo = null;
            Usuario u = null;
            //UsuarioAcesso ua = null;

            UsuarioVO model =
            Session.QueryOver<Usuario>(() => u)
                .SelectList(list => list
                    .Select(() => u.Id).WithAlias(() => vo.Id)
                    .Select(() => u.Nome).WithAlias(() => vo.Nome)
                    .Select(() => u.DataNascimento).WithAlias(() => vo.DataNascimento)
                    .Select(() => u.NumeroCPF).WithAlias(() => vo.NumeroCPF)
                    //.Select(() => u.Sexo.Id).WithAlias(() => vo.SexoId)
                    .Select(() => u.Nivel).WithAlias(() => vo.Nivel)
                    .Select(() => u.Ativo).WithAlias(() => vo.Ativo)
                    .Select(() => u.NomeUsuario).WithAlias(() => vo.NomeUsuario)
                    .Select(() => u.SenhaAcesso).WithAlias(() => vo.SenhaAcesso)
                    .Select(() => u.Email).WithAlias(() => vo.Email)
                ).Where(() => u.NomeUsuario == nomeUsuario)
                .And(() => u.SenhaAcesso == senhaAcesso)
                .TransformUsing(Transformers.AliasToBean<UsuarioVO>())
                .SingleOrDefault<UsuarioVO>();

            return model;
        }

        public IEnumerable<Usuario> GetListagem()
        {
            IQueryOver<Usuario> q = Session.QueryOver<Usuario>();
            IEnumerable<Usuario> lista;

            lista = q.List<Usuario>().ToList().OrderBy(x => x.Nome);
            return lista;
        }

        public UsuarioVO GetVOById(int id)
        {
            UsuarioVO fvo = null;
            Usuario f = null;

            UsuarioVO model =
            Session.QueryOver<Usuario>(() => f)
                .SelectList(list => list
                    .Select(() => f.Id).WithAlias(() => fvo.Id)
                    .Select(() => f.Nome).WithAlias(() => fvo.Nome)
                    .Select(() => f.DataNascimento).WithAlias(() => fvo.DataNascimento)
                    .Select(() => f.NumeroCPF).WithAlias(() => fvo.NumeroCPF)
                    //.Select(() => f.Sexo.Id).WithAlias(() => fvo.SexoId)
                    .Select(() => f.Nivel).WithAlias(() => fvo.Nivel)
                    .Select(() => f.Ativo).WithAlias(() => fvo.Ativo)
                    .Select(() => f.NomeUsuario).WithAlias(() => fvo.NomeUsuario)
                    //.Select(() => f.SenhaAcesso).WithAlias(() => fvo.SenhaAcesso)
                    .Select(() => f.Email).WithAlias(() => fvo.Email)
                ).Where(() => f.Id == id)
                .TransformUsing(Transformers.AliasToBean<UsuarioVO>())
                .SingleOrDefault<UsuarioVO>();

            return model;
        }

        public UsuarioAcaoVO GetUsuarioAcao(int usuarioId)
        {
            UsuarioAcaoVO pd = new UsuarioAcaoVO();
            pd.Id = usuarioId;
            pd.UsuarioId = usuarioId;

            IEnumerable<UsuarioAcao> itens =
                Session.QueryOver<UsuarioAcao>().Where(x => x.Usuario.Id == usuarioId).List();
            List<string> lista = new List<string>();
            foreach (UsuarioAcao i in itens)
            {
                string acao = i.Acao.Id;
                lista.Add(acao);
            }
            pd.ListaAcoes = lista.ToArray();

            return pd;
        }

        public void GravarAcoes(UsuarioAcaoVO usuario)
        {
            try
            {
                IEnumerable<UsuarioAcao> itens =
                    Session.QueryOver<UsuarioAcao>().Where(x => x.Usuario.Id == usuario.Id).List();

                foreach (UsuarioAcao i in itens)
                {
                    Session.Delete(i);
                }

                Usuario e = GetById(usuario.Id);
                AcaoDAO idao = new AcaoDAO();

                foreach(String a in usuario.ListaAcoes)
                {
                    Acao item = idao.GetByIdString(a);
                    if ((e != null) && (item != null))
                    {
                        Session.Save(new UsuarioAcao() { Usuario = e, Acao = item });
                    }
                }
            }
            catch (Exception e)
            {
            }
        }



    }
}
