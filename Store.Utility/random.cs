using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Utility
{
    public class random : IdentityUser
    {
        public string Name { get; set; }
    }
}
