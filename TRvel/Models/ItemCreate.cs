using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRvel.Models
{
    public class ItemCreate
    {
        public int ID { get; set; }
        public string ItemName { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
    }
}