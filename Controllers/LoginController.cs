using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using Project.Models;
using Project.Security;
using Project.Repositories;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        public object Post(
            [FromBody]User user,
            [FromServices]UserRepository repoUser,
            [FromServices]SigningConfigurations signingConfigurations,
            [FromServices]TokenConfigurations tokenConfigurations
        )
        {
            bool credentialsValid = false;
            if (user != null && !String.IsNullOrWhiteSpace(user.Email)) {
                var userBase = repoUser.findByEmail(user.Email);
                credentialsValid = (userBase != null && 
                    user.Email == userBase.Email &&
                    user.Password == userBase.Password);
            }

            if (credentialsValid) {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Email, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
                    }
                );

                DateTime dateCreate = DateTime.Now;
                DateTime dateExpiration = dateCreate + 
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);
                
                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor{
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dateCreate,
                    Expires = dateExpiration
                });
                var token = handler.WriteToken(securityToken);

                return new {
                    authenticated = true,
                    created = dateCreate.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dateExpiration.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "OK"
                };
            } else {
                return new {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            }
        }
    }
}