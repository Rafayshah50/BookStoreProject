using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();

        public static implicit operator List<object>(Cart v)
        {
            throw new NotImplementedException();
        }
    }

}
