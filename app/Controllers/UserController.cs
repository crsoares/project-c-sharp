﻿using System;
using Project.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;
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
    [Authorize("Bearer")]
    public class UserController : ControllerBase
    {
        private UserRepository _repoUser;

        public UserController([FromServices]UserRepository repoUser)
        {
            _repoUser = repoUser;
        }

        [HttpGet("all")]
        public IEnumerable<User> Get()
        {
            return _repoUser.GetAll();
        }

        [HttpGet("find/{id}")]
        public async Task<dynamic> GetUser(string id)
        {
            var result = await _repoUser.GetById(id);

            if (result == null) {
                return NotFound();
            }

            return result;
        }

        [HttpPost("store")]
        public dynamic Post(User user)
        {
            return _repoUser.create(user);
        }

        [HttpPut("update/{id}")]
        public async Task<dynamic> Put(string id, User user)
        {
            var result = await _repoUser.Update(id, user);
            
            if (result == false) {
                return NotFound();
            }

            return "Usuário alterado!";
        }

        [HttpDelete("destroy/{id}")]
         public async Task<dynamic> Delete(string id)
        {
            var result = await _repoUser.Delete(id);

            if (result) {
                return "Usuário excluido!";
            }

            return NotFound();
        }
    }
}