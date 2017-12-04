using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.DummyCommerce.Domain.DomainModels
{
    public class Item
    {
        public Guid ItemGuid { get; set; }          // A unique identifier for the item.
        public int Quantity { get; set; }           // Item's quantity.
    }

}
