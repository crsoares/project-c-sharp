using Project.Models;
using Project.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        public object Post(
            [FromBody]User user,
            [FromServices]AccessManager accessManager
        )
        {
            if (accessManager.ValidateCredentials(user)) {
                return accessManager.GenerateToken(user);
            } else {
                return new {
                    Authenticated = false,
                    Message = "Falha ao autenticar"
                };
            }
        }
    }
}