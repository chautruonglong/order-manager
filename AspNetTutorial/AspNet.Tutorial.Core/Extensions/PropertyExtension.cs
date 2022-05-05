using AspNet.Tutorial.Core.Entities;

using AutoMapper;

namespace AspNet.Tutorial.Core.Extensions
{
    public static class PropertyExtension
    {
        public static void CopyPropertiesTo<TEntity>(this TEntity src, ref TEntity dest) where TEntity : BaseEntity
        {
            MapperConfiguration config = new(config =>
            {
                config.CreateMap<TEntity, TEntity>();
                ;
            });

            IMapper mapper = config.CreateMapper();

            dest = mapper.Map<TEntity>(src);
        }

        public static void CopyPartialPropertiesTo<TEntity>(this TEntity src, ref TEntity dest) where TEntity : BaseEntity
        {
            MapperConfiguration config = new(config =>
            {
                config.CreateMap<TEntity, TEntity>()
                    .ForMember(e => e.Id, option => option.Ignore())
                    .ForMember(e => e.CreatedBy, option => option.Ignore())
                    .ForMember(e => e.ModifiedBy, option => option.Ignore())
                    .ForMember(e => e.IsDeleted, option => option.Ignore())
                    .ForMember(e => e.CreatedAt, option => option.Ignore())
                    .ForMember(e => e.ModifiedAt, option => option.Ignore());
            });

            IMapper mapper = config.CreateMapper();

            dest = mapper.Map<TEntity>(src);
        }
    }
}
