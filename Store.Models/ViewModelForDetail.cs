using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    public class ViewModelForDetail
    {
        public string UserName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
