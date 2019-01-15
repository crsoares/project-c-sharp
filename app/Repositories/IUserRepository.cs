using Project.Models;
using Project.Contracts;

namespace Project.Repositories
{
    public interface IUserRepository : IRepository
    {
        UserData findByEmail(string email);
    }
}