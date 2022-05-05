using System;
using System.Threading.Tasks;

using AspNet.Tutorial.Core.DataTransfers.Users;
using AspNet.Tutorial.Core.Entities;
using AspNet.Tutorial.Core.Interfaces.IRepositories;
using AspNet.Tutorial.Core.Interfaces.IServices;

using AutoMapper;

namespace AspNet.Tutorial.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<User> Get(Guid id)
        {
            return await _userRepository.Get(id);
        }

        public async Task<User> Update(Guid id, UserModificationDto userModificationDto)
        {
            User user = _mapper.Map<User>(userModificationDto);
            user.Id = id;

            return await _userRepository.Update(user);
        }

        public async Task<User> Delete(Guid id)
        {
            return await _userRepository.SoftDelete(id);
        }
    }
}
