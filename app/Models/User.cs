using Microsoft.AspNetCore.Identity;

namespace Project.Models
{
    public class User : IdentityUser
    {
        public override string PasswordHash {
            get => base.PasswordHash;
            set => base.PasswordHash = value;
        }
    }
}