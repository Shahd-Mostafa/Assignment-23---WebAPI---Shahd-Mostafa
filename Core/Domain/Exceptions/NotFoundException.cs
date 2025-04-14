using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(int id) : base($"Product with id:{id} was not found")
        {
        }
    }
}
