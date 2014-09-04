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
        public string GetNivelByUsuario(string nome)
        {
            Usuario u = null;
            UsuarioAcesso ua = null;
            string model =
            Session.QueryOver<UsuarioAcesso>(() => ua)
                .Inner.JoinQueryOver<Usuario>(() => ua.Usuario, () => u)
                .Where(() => ua.NomeUsuario == nome)
                .Select(x => u.Nivel)
                .Take(1)
                .SingleOrDefault<string>();
            return model;
        }

        public UsuarioVO GetUsuarioByNomeSenha(string nomeUsuario, string senhaAcesso)
        {
            UsuarioVO vo = null;
            Usuario u = null;
            UsuarioAcesso ua = null;

            UsuarioVO model =
            Session.QueryOver<UsuarioAcesso>(() => ua)
                .Inner.JoinQueryOver<Usuario>(() => ua.Usuario, () => u)
                .SelectList(list => list
                    .Select(() => u.Id).WithAlias(() => vo.Id)
                    .Select(() => u.Nome).WithAlias(() => vo.Nome)
                    .Select(() => u.DataNascimento).WithAlias(() => vo.DataNascimento)
                    .Select(() => u.NumeroCPF).WithAlias(() => vo.NumeroCPF)
                    .Select(() => u.Sexo.Id).WithAlias(() => vo.SexoId)
                    .Select(() => u.Nivel).WithAlias(() => vo.Nivel)
                    .Select(() => u.Ativo).WithAlias(() => vo.Ativo)
                    .Select(() => ua.NomeUsuario).WithAlias(() => vo.NomeUsuario)
                    .Select(() => ua.SenhaAcesso).WithAlias(() => vo.SenhaAcesso)
                    .Select(() => ua.Email).WithAlias(() => vo.Email)
                ).Where(() => ua.NomeUsuario == nomeUsuario)
                .And(() => ua.SenhaAcesso == senhaAcesso)
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
            UsuarioAcesso fe = null;

            UsuarioVO model =
            Session.QueryOver<Usuario>(() => f)
                .Left.JoinQueryOver<UsuarioAcesso>(() => f.Acesso, () => fe)
                .SelectList(list => list
                    .Select(() => f.Id).WithAlias(() => fvo.Id)
                    .Select(() => f.Nome).WithAlias(() => fvo.Nome)
                    .Select(() => f.DataNascimento).WithAlias(() => fvo.DataNascimento)
                    .Select(() => f.NumeroCPF).WithAlias(() => fvo.NumeroCPF)
                    .Select(() => f.Sexo.Id).WithAlias(() => fvo.SexoId)
                    .Select(() => f.Nivel).WithAlias(() => fvo.Nivel)
                    .Select(() => f.Ativo).WithAlias(() => fvo.Ativo)
                    .Select(() => fe.NomeUsuario).WithAlias(() => fvo.NomeUsuario)
                    .Select(() => fe.SenhaAcesso).WithAlias(() => fvo.SenhaAcesso)
                    .Select(() => fe.Email).WithAlias(() => fvo.Email)
                ).Where(() => f.Id == id)
                .TransformUsing(Transformers.AliasToBean<UsuarioVO>())
                .SingleOrDefault<UsuarioVO>();

            return model;
        }

    }
}
