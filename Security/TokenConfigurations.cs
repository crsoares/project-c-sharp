using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Project.Security
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}