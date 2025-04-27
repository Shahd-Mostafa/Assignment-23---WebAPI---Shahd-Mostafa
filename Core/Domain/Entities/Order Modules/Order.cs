using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Order_Modules
{
    public class Order : BaseEntity<int>
    {
        public Order()
        {
            
        }
        public Order(string userEmail, OrderAddress address, decimal subTotal, DeliveryMethod deliveryMethod, string paymentIntentId,IEnumerable<OrderItems> orderItems)
        {
            UserEmail = userEmail;
            Address = address;
            SubTotal = subTotal;
            DeliveryMethod = deliveryMethod;
            PaymentIntentId = paymentIntentId;
            OrderItems = orderItems;
        }

        public Order(string userEmail, OrderAddress address, decimal subTotal, DeliveryMethod deliveryMethod, IEnumerable<OrderItems> orderItems)
        {
            UserEmail = userEmail;
            Address = address;
            SubTotal = subTotal;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
        }

        public string UserEmail { get; set; }
        public OrderAddress Address { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public DeliveryMethod? DeliveryMethod { get; set; } // nav property
        public int? DeliveryMethodId { get; set; }
        public string? PaymentIntentId { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public IEnumerable<OrderItems> OrderItems { get; set; } // nav property
    }
}
