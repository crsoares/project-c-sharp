using System;
using Project.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _config;

        public UserController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpGet("all")]
        public IEnumerable<User> Get()
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("Project")))
            {
                return conexao.GetAll<User>();
            }
        }
    }
}