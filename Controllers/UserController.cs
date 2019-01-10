using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Dapper;
using Project.Models;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<User> Get([FromServices]IConfiguration configuration)
        {
            using (SqlConnection conexao = new SqlConnection(configuration.GetConnectionString("Project")))
            {
                return conexao.Query<User>("SELECT * FROM dbo.users");
            }
        }
    }
}