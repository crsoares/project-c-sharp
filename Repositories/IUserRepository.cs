using Project.Models;
using Project.Contracts;

namespace Project.Repositories
{
    public interface IUserRepository : IRepository
    {
        User findByEmail(string email);
    }
}