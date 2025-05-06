using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Order_Modules
{
    public enum PaymentStatus
    {
        Pending = 0,
        PaymentRecieved,
        PaymentFailed,
    }
}
