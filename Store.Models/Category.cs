using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    public class Category
    {
            [Key]
            public int ID { get; set; }
            [Required]
            [MaxLength(25)]
            public string Name { get; set; }
            [Required]
            [DisplayName("Display Order")]
            [Range(1, 100, ErrorMessage = "Display order must be between 1-100")]
            public int DisplayOrder { get; set; }
    }
}

