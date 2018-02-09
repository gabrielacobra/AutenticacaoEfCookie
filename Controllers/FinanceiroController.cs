using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutenticacaoEfCookie.Controllers
{
    [Authorize(Roles="Financeiro")] //só pode acessar a página se estiver autenticado, se não estiver, vai te direcionar para a página de login e senha
    public class FinanceiroController:Controller
    {
        
        public IActionResult Index(){
            return View();
        }
    }
}