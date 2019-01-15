using System;
using Project.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Project.Repositories;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _repoUser;

        public UserController([FromServices]UserRepository repoUser)
        {
            _repoUser = repoUser;
        }

        // [Authorize("Bearer")]
        [HttpGet("all")]
        public IEnumerable<User> Get()
        {
            return _repoUser.all();
        }

        [Authorize("Bearer")]
        [HttpGet("find/{id}")]
        public User GetUser(int id)
        {
            return _repoUser.find(id);
        }

        // [Authorize("Bearer")]
        [HttpPost("store")]
        public dynamic Post(UserData user)
        {
            return _repoUser.create(user);
        }

        [Authorize("Bearer")]
        [HttpPut("update/{id}")]
        public dynamic Put(int id, UserData user)
        {
            var result = _repoUser.update(id, user);

            if (result) {
                return user;
            }

            return BadRequest();
        }

        [Authorize("Bearer")]
        [HttpDelete("destroy/{id}")]
        public dynamic Delete(int id)
        {
            return _repoUser.delete(id);
        }
    }
}