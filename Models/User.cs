using System;
using Dapper.Contrib.Extensions;

namespace Project.Models
{
    [Table("dbo.users")]
    public class User
    {
        [ExplicitKey]
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}