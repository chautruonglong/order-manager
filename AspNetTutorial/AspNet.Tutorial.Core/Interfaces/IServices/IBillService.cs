using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AspNet.Tutorial.Core.DataTransfers.Bills;

namespace AspNet.Tutorial.Core.Interfaces.IServices
{
    public interface IBillService : IService
    {
        Task CreateBillOrder(Guid userId, BillCreationDto billCreationDto);

        Task<IEnumerable<BillOrderDto>> GetBillOrders(Guid userId, int page, int size);
    }
}
