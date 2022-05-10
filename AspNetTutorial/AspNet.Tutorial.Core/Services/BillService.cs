using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AspNet.Tutorial.Core.DataTransfers.Bills;
using AspNet.Tutorial.Core.DataTransfers.Orders;
using AspNet.Tutorial.Core.Entities;
using AspNet.Tutorial.Core.Enums;
using AspNet.Tutorial.Core.Interfaces.IRepositories;
using AspNet.Tutorial.Core.Interfaces.IServices;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

namespace AspNet.Tutorial.Core.Services
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _billRepository;

        private readonly IOrderRepository _orderRepository;

        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;

        public BillService(IBillRepository billRepository, IOrderRepository orderRepository, IProductRepository productRepository, IMapper mapper)
        {
            _billRepository = billRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        
        public async Task CreateBillOrder(Guid userId, BillCreationDto billCreationDto)
        {
            var bill = new Bill { Total = billCreationDto.Total };
            bill = await _billRepository.Insert(bill);

            var orders = new List<Order>();
            foreach (var orderCreationDto in billCreationDto.Orders)
            {
                orders.Add( new Order
                {
                    UserId = userId,
                    ProductId = orderCreationDto.Product.Id,
                    BillId = bill.Id,
                    Quantity = orderCreationDto.Quantity
                });
            }

            await _orderRepository.InsertMany(orders);
        }

        public async Task<IEnumerable<BillOrderDto>> GetBillOrders(Guid userId, int page, int size)
        {
            var orders = await _orderRepository.GetMany(page, size, o => o.ModifiedAt, SortDirections.Desc)
                .Where(o => o.UserId.Equals(userId))
                .Include(o => o.Bill)
                .Include(o => o.Product)
                .ToListAsync();
            
            if (orders == null)
            {
                return null;
            }
            
            var bills = new HashSet<Bill>();
            orders.ForEach(o => bills.Add(o.Bill));
            
            var billOrderDtoList = _mapper.Map<List<BillOrderDto>>(bills.ToList());
            billOrderDtoList.ForEach(bill => bill.Orders = _mapper.Map<IEnumerable<OrderProductDto>>(orders.FindAll(o => o.Bill.Id == bill.Id)));

            return billOrderDtoList;
        }
    }
}
