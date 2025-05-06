using Domain.Entities.Order_Modules;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Services.Specification;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PaymentService (IBasketRepository _basketRepository,
        IUnitOfWork _unitOfWork,
        IMapper _mapper,
        IConfiguration _configuration) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            var basket = await _basketRepository.GetBasketByIdAsync(basketId)
                        ?? throw new BasketNotFoundException(basketId);
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Products,int>().GetAsync(item.Id)
                              ?? throw new ProductNotFoundException(item.Id);
                if (product == null)
                    throw new NotFoundException($"Product with id:{item.Id} was not found");
                item.Price = product.Price;
            }
            if(!basket.DeliveryMethodId.HasValue) throw new BadRequestException("Delivery method must be selected");
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetAsync(basket.DeliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);
            basket.ShippingPrice = deliveryMethod.Price;
            var service = new PaymentIntentService();
            var amount = (long) (basket.Items.Sum(i => i.Price * i.Quantity) + deliveryMethod.Price) *100;
            if(string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                var paymentIntent = await service.CreateAsync(createOptions);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                };
                var paymentIntent = await service.UpdateAsync(basket.PaymentIntentId, updateOptions);
            }
            await _basketRepository.UpdateBasketAsync(basket);
            return _mapper.Map<BasketDto>(basket);
        }

        public async Task UpdateOrderPaymentStatus(string requestBody, string signature)
        {
            try
            {
                var endpointSecret = _configuration["Stripe:EndPointSecret"];
                var stripeEvent = EventUtility.ConstructEvent(requestBody,
                signature, endpointSecret);
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
                {
                    await UpdatePaymentStatus(paymentIntent!.Id, PaymentStatus.PaymentRecieved);
                }
                else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
                {
                    await UpdatePaymentStatus(paymentIntent!.Id, PaymentStatus.PaymentFailed);
                }
                else
                {
                    Console.WriteLine($"Unhandled event type: {stripeEvent.Type}");
                }
            }
            catch (StripeException ex)
            {
                Console.WriteLine($"Stripe webhook error: {ex.Message}");
            }
        }
        private async Task UpdatePaymentStatus(string paymentIntentId, PaymentStatus status)
        {
            var order = await _unitOfWork.GetRepository<Order, int>()
                .GetAsync(new OrderWithPaymentIntentSpecification(paymentIntentId))
                ?? throw new BadRequestException("No order found with this payment id");

            order.PaymentStatus = status;
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
