using Domain.Entities.Order_Modules;
using Domain.Exceptions;
using Services.Specification;
using Shared.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService(IUnitOfWork _unitOfWork, IMapper _mapper,IBasketRepository _basketRepository) : IOrderService
    {
        public async Task<OrderResultDto> CreateOrderAsync(OrderRequest orderRequest, string email)
        {
            var address = _mapper.Map<OrderAddress>(orderRequest.ShippingAddress);
            var basket = await _basketRepository.GetBasketByIdAsync(orderRequest.BasketId) ?? throw new BasketNotFoundException(orderRequest.BasketId);
            var orderItems = new List<OrderItems>();
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Products,int>().GetAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(item, product));
            }
            var deliveryMethod= await _unitOfWork.GetRepository<DeliveryMethod,int>().GetAsync(orderRequest.DeliveryMethodId) ??
                throw new DeliveryMethodNotFoundException(orderRequest.DeliveryMethodId);
            var subTotal = orderItems.Sum(p => p.Quantity * p.Price);

            var existingOrder = await _unitOfWork.GetRepository<Order, int>().GetAsync(new OrderWithPaymentIntentSpecification(basket.PaymentIntentId!));
            if(existingOrder != null)
            {
                _unitOfWork.GetRepository<Order, int>().Delete(existingOrder);
            }
            var order = new Order(email, address, subTotal, deliveryMethod, basket.PaymentIntentId!, orderItems);
            await _unitOfWork.GetRepository<Order,int>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            var orderResult = _mapper.Map<OrderResultDto>(order);
            return orderResult;
        }

        private OrderItems CreateOrderItem(BasketItem item, Products product)
            => new OrderItems(new ProductOrderItem(product.Id,product.Name,product.PictureUrl), item.Quantity,item.Price);

        public async Task<List<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            var methods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            var deliveryMethods = _mapper.Map<List<DeliveryMethodResult>>(methods);
            return deliveryMethods;
        }

        public async Task<OrderResultDto> GetOrderByIdAsync(int id)
        {
            var order = await _unitOfWork.GetRepository<Order, int>().GetAsync(new OrderWithSpecification(id)) ?? throw new OrderNotFoundException(id);
            return _mapper.Map<OrderResultDto>(order);
        }

        public async Task<List<OrderResultDto>> GetOrdersForUserAsync(string Email)
        {
            var order = await _unitOfWork.GetRepository<Order, int>().GetAllAsync(new OrderWithSpecification(Email)) ?? throw new OrderNotFoundException(Email);
            return _mapper.Map<List<OrderResultDto>>(order);
        }
    }
}
