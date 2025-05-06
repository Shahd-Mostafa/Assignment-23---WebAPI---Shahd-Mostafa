using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Order_Modules
{
    public class ProductOrderItem : BaseEntity<int>
    {
        public ProductOrderItem()
        {
            
        }
        public ProductOrderItem(int orderId, string name, string pictureUrl)
        {
            OrderId = orderId;
            Name = name;
            PictureUrl = pictureUrl;
        }

        public int OrderId { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
    }
}
