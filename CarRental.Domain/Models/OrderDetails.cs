using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Domain.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public Service Service { get; set; }
        public int Quantity { get; set; }
    }
}
