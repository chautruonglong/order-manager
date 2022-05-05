using AspNet.Tutorial.Core.DataTransfers.Bills;
using AspNet.Tutorial.Core.Entities;

using AutoMapper;

namespace AspNet.Tutorial.Core.Profiles
{
    public class BillProfile : Profile
    {
        public BillProfile()
        {
            CreateMap<Bill, BillOrderDto>();
        }
    }
}
