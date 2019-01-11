using System;
using Dapper;
using System.Linq;
using Project.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Project.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository 
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
            
        }

        public dynamic create(dynamic obj)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("Project")))
            {
                var objUser = new User(){
                    Name = obj.Name,
                    Email = obj.Email,
                    Password = obj.Password
                };
                
                return conexao.Insert(objUser);
            }
        }

        public dynamic update(int id, dynamic obj)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("Project")))
            {
                var objUser = new User(){
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
                return conexao.Delete(new User(){ Id = id });
            }
        }

        public User findByEmail(string email)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("Project")))
            {
                return conexao.QueryFirstOrDefault<User>(
                    "SELECT id, name, email, password " +
                    "FROM dbo.users " +
                    "WHERE email = @Email", new { Email = email }
                );
            }
        }
    }
}