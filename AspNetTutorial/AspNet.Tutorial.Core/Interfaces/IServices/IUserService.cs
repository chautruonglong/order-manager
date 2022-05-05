using System;
using System.Threading.Tasks;

using AspNet.Tutorial.Core.DataTransfers.Users;
using AspNet.Tutorial.Core.Entities;

namespace AspNet.Tutorial.Core.Interfaces.IServices
{
    public interface IUserService : IService
    {
        Task<User> Get(Guid id);

        Task<User> Update(Guid id, UserModificationDto userModificationDto);

        Task<User> Delete(Guid id);
    }
}
