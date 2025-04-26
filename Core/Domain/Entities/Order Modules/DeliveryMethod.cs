using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Order_Modules
{
    public class DeliveryMethod : BaseEntity<int>
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string shortName, string deliveryTime, decimal price, string description)
        {
            ShortName = shortName;
            DeliveryTime = deliveryTime;
            Price = price;
            Description = description;
        }

        public string ShortName { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
