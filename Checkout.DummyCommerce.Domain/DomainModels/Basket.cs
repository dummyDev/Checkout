using System;
using System.Collections.Generic;

namespace Checkout.DummyCommerce.Domain.DomainModels
{
    public class Basket
    {
        public Guid BasketGuid { get; set; }          // A unique identifier for the cart.
        public string UserId { get; set; }            // Customer Identifier; if not present, user-id associated with the accessToken will be used.
        public string SessionId { get; set; }         // A placeholder for apps to store any info that will help map the user.
        public List<Item> BasketItems { get; set; }  // Basket's items.
        public DateTime CreatedAt { get; set; }       // Created date/time.
        public DateTime UpdatedAt { get; set; }       // Updated date/time.
    }
}
