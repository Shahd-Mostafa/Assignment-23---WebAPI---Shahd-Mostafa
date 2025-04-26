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
        public Order(string userEmail, OrderAddress address, decimal subTotal, decimal total, PaymentStatus paymentStatus, DeliveryMethod deliveryMethod, string paymentIntentId, DateTimeOffset orderDate, IEnumerable<OrderItems> orderItems)
        {
            UserEmail = userEmail;
            Address = address;
            SubTotal = subTotal;
            Total = total;
            PaymentStatus = paymentStatus;
            DeliveryMethod = deliveryMethod;
            PaymentIntentId = paymentIntentId;
            OrderDate = orderDate;
            OrderItems = orderItems;
        }

        public Order(string userEmail, OrderAddress address, decimal subTotal, decimal total, PaymentStatus paymentStatus, DeliveryMethod deliveryMethod, DateTimeOffset orderDate, IEnumerable<OrderItems> orderItems)
        {
            UserEmail = userEmail;
            Address = address;
            SubTotal = subTotal;
            Total = total;
            PaymentStatus = paymentStatus;
            DeliveryMethod = deliveryMethod;
            OrderDate = orderDate;
            OrderItems = orderItems;
        }

        public string UserEmail { get; set; }
        public OrderAddress Address { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DeliveryMethod? DeliveryMethod { get; set; } // nav property
        public int? DeliveryMethodId { get; set; }
        public string PaymentIntentId { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public IEnumerable<OrderItems> OrderItems { get; set; } // nav property
    }
}
