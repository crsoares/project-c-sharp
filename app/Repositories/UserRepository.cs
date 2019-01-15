using System;
using Dapper;
using System.Linq;
using Project.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Identity;
using Project.Data;

namespace Project.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository 
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public UserRepository(
            ApplicationDbContext context,
            UserManager<User> userManager,
            IConfiguration configuration
        ) : base(configuration)
        {
            _context = context;
            _userManager = userManager;
        }

        new public dynamic all()
        {
            return _context.Users.ToList();
        }

        public dynamic create(dynamic obj)
        {
            var user = new User(){
                    UserName = obj.Name,
                    Email = obj.Email,
                    EmailConfirmed = true
                };

            var result = _userManager
                .CreateAsync(user, obj.Password)
                .Result;

            return result.Succeeded;
        }

        public dynamic update(int id, dynamic obj)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("Project")))
            {
                var objUser = new UserData(){
                    Id = id,
                    Name = obj.Name,
                    Email = obj.Email,
                    Password = obj.Password
                };
                
                return conexao.Update(objUser);
            }
        }

        public dynamic delete(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("Project")))
            {
                return conexao.Delete(new UserData(){ Id = id });
            }
        }

        public UserData findByEmail(string email)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("Project")))
            {
                return conexao.QueryFirstOrDefault<UserData>(
                    "SELECT id, name, email, password " +
                    "FROM dbo.users " +
                    "WHERE email = @Email", new { Email = email }
                );
            }
        }
    }
}