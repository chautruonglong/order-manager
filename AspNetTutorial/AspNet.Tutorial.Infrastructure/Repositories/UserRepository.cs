using System;
using System.Security.Authentication;
using System.Threading.Tasks;

using AspNet.Tutorial.Core.Entities;
using AspNet.Tutorial.Core.Interfaces.IRepositories;
using AspNet.Tutorial.Infrastructure.Contexts;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

namespace AspNet.Tutorial.Infrastructure.Repositories
{
    public class UserRepository : CrudRepository<User, Guid>, IUserRepository
    {
        private readonly AspNetTutorialContext _context;

        private readonly DbSet<User> _users;

        public UserRepository(AspNetTutorialContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _users = context.Users;
        }

        public async Task<User> GetByEmail(string email)
        {
            User user = await _users.FirstOrDefaultAsync(u => u.Email.Equals(email));

            if (user == null || user.IsDeleted)
            {
                throw new AuthenticationException("User not found in database");
            }

            return user;
        }
    }
}
