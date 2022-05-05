using System.Threading.Tasks;

using AspNet.Tutorial.Core.DataTransfers.Auths;
using AspNet.Tutorial.Core.Entities;

namespace AspNet.Tutorial.Core.Interfaces.IServices
{
    public interface IAuthService : IService
    {
        Task<AccessTokenDto> VerifyLogin(string email, string password);

        Task<User> Register(UserRegisterDto userRegisterDto);

        Task<AccessTokenDto> RefreshToken(string token);
    }
}
