using System;
using System.Threading.Tasks;

using AspNet.Tutorial.Core.Entities;

namespace AspNet.Tutorial.Core.Interfaces.IRepositories
{
    public interface IUserRepository : ICrudRepository<User, Guid>
    {
        Task<User> GetByEmail(string email);
    }
}
