using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutenticacaoEfCookie.Dados;
using AutenticacaoEfCookie.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutenticacaoEfCookie.Controllers
{
    public class ContaController : Controller
    {
        readonly AutenticacaoContexto _contexto;

        public ContaController(AutenticacaoContexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario usuario)
        {
            try
            {
                Usuario user = _contexto.Usuarios.Include("UsuarioPermissao").Include("UsuariosPermissao.Permissao").FirstOrDefault(c => c.Email == usuario.Email && c.Senha == usuario.Senha);

                if (user != null)
                {
                    var claims = new List<Claim>();

                    claims.Add(new Claim(ClaimTypes.Email, user.Email));
                    claims.Add(new Claim(ClaimTypes.Name, user.Nome));
                    claims.Add(new Claim(ClaimTypes.Sid, user.IdUsuario.ToString()));

                    foreach(var item in user.UsuarioPermissao){
                        claims.Add(new Claim(ClaimTypes.Role, item.Permissao.Nome));
                    }

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Financeiro");
                }
                return View(usuario);

            }
            catch (Exception)
            {

                return null;
            }
        }

        public IActionResult Sair()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}