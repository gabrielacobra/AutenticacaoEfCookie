using System.Linq;
using AutenticacaoEfCookie.Models;

namespace AutenticacaoEfCookie.Dados
{
    public class CodeFirstBanco
    {
        public static void Inicializar (AutenticacaoContexto contexto){
            contexto.Database.EnsureCreated();

            if(contexto.Usuarios.Any())return; //para garantir que não serão criados usuários duplicados

            var usuario = new Usuario(){
                Nome = "Gabriela",
                Email = "gabicobra@gmail.com",
                Senha = "123456"
            };

            contexto.Usuarios.Add(usuario);

            var permissao = new Permissao(){
                Nome = "Financeiro"
            };

            contexto.Permissoes.Add(permissao);

            var usuariopermissao = new UsuarioPermissao(){
                IdUsuario = usuario.IdUsuario,
                IdPermissao = permissao.IdPermissao
            };

            contexto.UsuariosPermissoes.Add(usuariopermissao);
            contexto.SaveChanges();

        }
    }
}