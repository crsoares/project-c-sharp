using Project.Models;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;

namespace Project.Repositories
{
    public class BaseRepository
    {
        protected IConfiguration _config;
         public BaseRepository(IConfiguration configuration)
         {
             _config = configuration;
         }

         public dynamic all()
         {
             using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("Project")))
             {
                return conexao.GetAll<User>();
             }
         }

         public dynamic find(int id)
         {
             using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("Project")))
             {
                return conexao.Get<User>(id);
             }
         }
    }
}