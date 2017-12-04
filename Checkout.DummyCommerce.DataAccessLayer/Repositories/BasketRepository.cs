using System;
using System.Collections.Generic;
using System.Text;
using Checkout.DummyCommerce.Domain.DomainModels;

namespace Checkout.DummyCommerce.DataAccessLayer.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly List<Basket> _dummyDatabase;

        // TODO: Provision a Logger
        public BasketRepository()
        {
            _dummyDatabase = new List<Basket>
            {
                new Basket()
                {
                    BasketGuid = new Guid("ca761232-ed42-11ce-bacd-00aa0057b223"),
                    BasketItems = new List<Item>(),
                    UserId = "dummyUserId",
                    SessionId = "dummySessionId",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            };

        }

        public RepositoryReturnType<Basket> GetBasketById(Guid basketGuid)
        {
            Basket retrievedBasket;
            try
            {
                retrievedBasket = _dummyDatabase.Find(x => x.BasketGuid == basketGuid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new RepositoryReturnType<Basket>() { IsTransactionSuccess = false, Message = e.Message, RecordsAffected = 0, ReturnData = null };
            }
            return new RepositoryReturnType<Basket>() { IsTransactionSuccess = true, Message = "Basket retrieved from persistance.", RecordsAffected = 0, ReturnData = retrievedBasket };
        }

        public RepositoryReturnType<Basket> CreateBasket()
        {
            // TODO: Inject HttpSessionId
            var newBasket = new Basket()
            {
                BasketGuid = Guid.NewGuid(),
                BasketItems = new List<Item>(),
                UserId = "dummyUserId",
                SessionId = "dummySessionId",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            try
            {
                _dummyDatabase.Add(newBasket);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new RepositoryReturnType<Basket>() { IsTransactionSuccess = false, Message = e.Message, RecordsAffected = 0, ReturnData = null };
            }
            return new RepositoryReturnType<Basket>() { IsTransactionSuccess = true, Message = "Basket created on persistance.", RecordsAffected = 1, ReturnData = newBasket };
        }

        public RepositoryReturnType<Basket> AddBasketItem(Guid basketGuid, Item item)
        {
            Basket retrievedBasket;
            try
            {
                retrievedBasket = _dummyDatabase.Find(x => x.BasketGuid == basketGuid);
                retrievedBasket.BasketItems.Add(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new RepositoryReturnType<Basket>() { IsTransactionSuccess = false, Message = e.Message, RecordsAffected = 0, ReturnData = null };
            }
            return new RepositoryReturnType<Basket>() { IsTransactionSuccess = true, Message = "Basket created on persistance.", RecordsAffected = 1, ReturnData = retrievedBasket };
        }

        public RepositoryReturnType<Basket> DeleteBasket(Guid basketGuid)
        {
            int recordsRemoved;
            try
            {
                // TODO: Make operation more efficient
                recordsRemoved = _dummyDatabase.RemoveAll(x => x.BasketGuid == basketGuid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new RepositoryReturnType<Basket>() { IsTransactionSuccess = false, Message = e.Message, RecordsAffected = 0, ReturnData = null };
            }
            return new RepositoryReturnType<Basket>() { IsTransactionSuccess = true, Message = "Request succeded", RecordsAffected = recordsRemoved, ReturnData = null };
        }

        public RepositoryReturnType<Basket> DeleteBasketItem(Guid basketGuid, Item item)
        {
            Basket retrievedBasket;
            bool isRemoved;
            try
            {
                retrievedBasket = _dummyDatabase.Find(x => x.BasketGuid == basketGuid);
                isRemoved = retrievedBasket.BasketItems.Remove(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new RepositoryReturnType<Basket>() { IsTransactionSuccess = false, Message = e.Message, RecordsAffected = 0, ReturnData = null };
            }
            return new RepositoryReturnType<Basket>() { IsTransactionSuccess = true, Message = "Item removed from basket on persistance.", RecordsAffected = isRemoved? 1 : 0, ReturnData = retrievedBasket };
        }

        public RepositoryReturnType<Basket> UpdateBasket(Guid basketGuid)
        {
            throw new NotImplementedException();
        }

        public RepositoryReturnType<Basket> UpdateBasketItem(Guid basketGuid, Item item)
        {
            throw new NotImplementedException();
        }
    }
}
