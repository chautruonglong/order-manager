using AspNet.Tutorial.Core.DataTransfers.Auths;
using AspNet.Tutorial.Core.DataTransfers.Users;
using AspNet.Tutorial.Core.Entities;

using AutoMapper;

namespace AspNet.Tutorial.Core.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModificationDto, User>();

            CreateMap<UserRegisterDto, User>();

            CreateMap<User, UserDto>();

            CreateMap<User, User>()
                .ForMember(e => e.Id, option => option.Ignore())
                .ForMember(e => e.CreatedBy, option => option.Ignore())
                .ForMember(e => e.ModifiedBy, option => option.Ignore())
                .ForMember(e => e.IsDeleted, option => option.Ignore())
                .ForMember(e => e.CreatedAt, option => option.Ignore())
                .ForMember(e => e.ModifiedAt, option => option.Ignore());
        }
    }
}
