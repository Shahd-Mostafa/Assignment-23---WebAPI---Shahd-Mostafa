using Shared.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IOrderService
    {
        Task<OrderResultDto> GetOrderByIdAsync(int id);
        Task<List<OrderResultDto>> GetOrdersForUserAsync(string Email);
        Task<List<DeliveryMethodResult>> GetDeliveryMethodsAsync();
        Task<OrderResultDto> CreateOrderAsync(OrderRequest orderRequest, string Email);
    }
}
