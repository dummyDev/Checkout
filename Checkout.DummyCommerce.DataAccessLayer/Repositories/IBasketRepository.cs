using System;
using Checkout.DummyCommerce.Domain.DomainModels;

namespace Checkout.DummyCommerce.DataAccessLayer.Repositories
{
    public interface IBasketRepository
    {
        // GET: Get basket details from persistance
        RepositoryReturnType<Basket> GetBasketById(Guid id);

        // POST: Add new basket to persistance
        RepositoryReturnType<Basket> CreateBasket();
        // POST: Add item to the persistance basket
        RepositoryReturnType<Basket> AddBasketItem(Guid basketGuid, Item item);

        // DELETE: Delete basket from persistance
        RepositoryReturnType<Basket> DeleteBasket(Guid basketGuid);
        // DELETE: Dekete item from persistance basket
        RepositoryReturnType<Basket> DeleteBasketItem(Guid basketGuid, Item item);

        // PUT: Update basket on persistance
        RepositoryReturnType<Basket> UpdateBasket(Guid basketGuid);
        // PUT: Update basket items on persistance
        RepositoryReturnType<Basket> UpdateBasketItem(Guid basketGuid, Item item);
    }
}
