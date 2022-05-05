using System;
using System.Threading.Tasks;

using AspNet.Tutorial.Core.DataTransfers.Bills;

namespace AspNet.Tutorial.Core.Interfaces.IServices
{
    public interface IBillService : IService
    {
        Task CreateBillOrder(Guid userId, BillCreationDto billCreationDto);

        Task<BillOrderDto> GetBillOrders(Guid userId, int page, int size);
    }
}
