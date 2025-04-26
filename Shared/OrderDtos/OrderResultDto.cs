using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderDtos
{
    public record OrderResultDto
    {
        public int Id { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public OrderAddressDto Address { get; set; }
        public decimal SubTotal { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
        public string DeliveryMethod { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public decimal Total { get; set; }
    }
}
