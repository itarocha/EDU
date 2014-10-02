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
    public class UsuarioDAO : GenericDAO<Usuario>
    {
        public bool UsuarioPossuiPermissao(string nome, string acaoId) {
            return true;
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

        public IEnumerable<Usuario> GetListaUsuarios(string searchString = null)
        {
            IQueryOver<Usuario> q = Session.QueryOver<Usuario>();
            IEnumerable<Usuario> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<Usuario>().Where(s => s.Nome.ToLower().Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<Usuario>().ToList().OrderBy(x => x.Nome);
            }
            return lista;
        }

        public UsuarioVO GetUsuarioVOById(int id)
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
                    .Select(() => f.SenhaAcesso).WithAlias(() => fvo.SenhaAcesso)
                    .Select(() => f.Email).WithAlias(() => fvo.Email)
                ).Where(() => f.Id == id)
                .TransformUsing(Transformers.AliasToBean<UsuarioVO>())
                .SingleOrDefault<UsuarioVO>();

            return model;
        }

    }
}
