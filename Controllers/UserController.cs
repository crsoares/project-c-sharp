using System;
using Project.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Project.Repositories;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _repoUser;

        public UserController(IUserRepository repoUser)
        {
            _repoUser = repoUser;
        }

        [HttpGet("all")]
        public IEnumerable<User> Get()
        {
            return _repoUser.all();
        }

        [HttpGet("find/{id}")]
        public User GetUser(int id)
        {
            return _repoUser.find(id);
        }

        [HttpPost("store")]
        public dynamic Post(User user)
        {
            return _repoUser.create(user);
        }

        [HttpPut("update/{id}")]
        public dynamic Put(int id, User user)
        {
            var result = _repoUser.update(id, user);

            if (result) {
                return user;
            }

            return BadRequest();
        }

        [HttpDelete("destroy/{id}")]
        public dynamic Delete(int id)
        {
            return _repoUser.delete(id);
        }
    }
}