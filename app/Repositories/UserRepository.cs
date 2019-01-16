using System;
using System.Linq;
using Project.Data;
using Project.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Project.Repositories
{
    public class UserRepository
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly UserManager<User> _userManager;
        public UserRepository(
            ApplicationDbContext dbContext,
            UserManager<User> userManager
        )
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IQueryable<User> GetAll()
        {
            return _dbContext.Set<User>().AsNoTracking();
        }

        public async Task<User> GetById(string id)
        {
            return await _dbContext.Set<User>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public dynamic create(User obj)
        {
            var result = _userManager
                .CreateAsync(obj, obj.PasswordHash)
                .Result;

            return result.Succeeded;
        }

        public async Task<dynamic> Update(string id, User obj)
        {
            var user = await _dbContext.Set<User>().FindAsync(id);

            if (user == null) {
                return false;
            } else {
                user.UserName = obj.UserName;
                user.Email = obj.Email;
                user.NormalizedUserName = obj.UserName;
                user.NormalizedEmail = obj.Email;
                await _userManager.UpdateAsync(user);

                return true;
            }
        }

        public async Task<dynamic> Delete(string id)
        {
            var entity = await _dbContext.Set<User>().FindAsync(id);

            if (entity == null) {
                return false;
            }

            _dbContext.Set<User>().Remove(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        /*public UserData findByEmail(string email)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("Project")))
            {
                return conexao.QueryFirstOrDefault<UserData>(
                    "SELECT id, name, email, password " +
                    "FROM dbo.users " +
                    "WHERE email = @Email", new { Email = email }
                );
            }
        }*/
    }
}