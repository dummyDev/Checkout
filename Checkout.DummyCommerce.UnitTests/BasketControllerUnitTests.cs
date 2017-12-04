using System;
using System.Collections.Generic;
using Checkout.DummyCommerce.DataAccessLayer.Repositories;
using Checkout.DummyCommerce.Domain.DomainModels;
using Checkout.DummyCommerce.RestApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Checkout.DummyCommerce.UnitTests
{
    [TestFixture]
    public class BasketControllerUnitTests
    {
        // Mock the repository and the web service
        private IBasketRepository _basketRepository;

        // Class to be unit tested
        private BasketController _basketController;

        [SetUp]
        public void StartUp()
        {
            // Mock
            _basketRepository = Substitute.For<IBasketRepository>();

            // Instantiate
            _basketController =
                new BasketController(_basketRepository);
        }

        [Test]
        public void CreateBasket_RepoCalled()
        {
            // arrange 

            // act
            var actionResult = _basketController.CreateBasket() as OkObjectResult;
            var response = actionResult?.Value as RepositoryReturnType<Basket>;

            // assert
            Assert.That(response.IsTransactionSuccess, Is.True);
        }

        // Write every possible TestCase
    }
}