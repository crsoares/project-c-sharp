using System;
using Project.Models;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Project.Security
{
    public class AccessManager
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private TokenConfigurations _tokenConfigurations;
        private SigningConfigurations _signingConfigurations;
        

        public AccessManager(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            TokenConfigurations tokenConfigurations,
            SigningConfigurations signingConfigurations
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenConfigurations = tokenConfigurations;
            _signingConfigurations = signingConfigurations;
        }

        public bool ValidateCredentials(User user)
        {
            bool credenciaisValidas = false;
            if (user != null && !String.IsNullOrWhiteSpace(user.UserName))
            {
                var userIdentity = _userManager
                    .FindByNameAsync(user.UserName).Result;
                if (userIdentity != null)
                {
                    var resultadoLogin = _signInManager
                        .CheckPasswordSignInAsync(userIdentity, user.PasswordHash, false)
                        .Result;
                    if (resultadoLogin.Succeeded)
                    {
                        credenciaisValidas = true;
                    }
                }
            }

            return credenciaisValidas;
        }

        public Token GenerateToken(User user)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.UserName, "Login"),
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                }
            );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao +
                TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            var token = handler.WriteToken(securityToken);

            return new Token()
            {
                Authenticated = true,
                Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                Message = "OK"
            };
        }
    }
}