using AspNet.Tutorial.Core.DataTransfers.Orders;
using AspNet.Tutorial.Core.Entities;

using AutoMapper;

namespace AspNet.Tutorial.Core.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderProductDto>();
        }
    }
}
