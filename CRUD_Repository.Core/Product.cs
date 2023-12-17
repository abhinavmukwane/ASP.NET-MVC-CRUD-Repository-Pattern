﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Repository.Core
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Product Name")]
        public string? ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Qty { get; set; }
    }
}
