using System;
using Dapper.Contrib.Extensions;

namespace Project.Models
{
    [Table("dbo.users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}