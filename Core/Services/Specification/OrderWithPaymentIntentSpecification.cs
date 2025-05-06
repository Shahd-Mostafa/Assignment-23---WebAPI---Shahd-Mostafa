using Domain.Entities.Order_Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    public class OrderWithPaymentIntentSpecification : Specifications<Order>
    {
        public OrderWithPaymentIntentSpecification(string PaymentIntentId) : base(o =>o.PaymentIntentId == PaymentIntentId)
        {
        }
    }
}
