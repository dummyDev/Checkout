using System;
using Checkout.DummyCommerce.DataAccessLayer.Repositories;
using Checkout.DummyCommerce.Domain.DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.DummyCommerce.RestApi.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// This API will allow our users to set up and manage an order of items.
    /// The API will allow users to add and remove items and change the quantity of the items they want.  
    /// They should also be able to simply clear out all items from their order and start again.
    /// </summary>
    [Route("v1/[controller]")]
    [Produces("application/json")]
    public class BasketController : Controller
    {
        private readonly IBasketRepository _basket;

        /// <summary>
        /// BasketController constructor method, arguments made available through DI
        /// </summary>
        public BasketController(IBasketRepository basket)
        {
            _basket = basket;
        }

        /// <summary>
        /// Get basket details from persistance.
        /// </summary>
        /// <remarks>
        ///  
        ///     GET /v1/basket/ca761232-ed42-11ce-bacd-00aa0057b223
        /// 
        /// </remarks>
        /// <returns>Basket based on it's Guid</returns>
        /// <response code="200">Returns retrieved basket</response>
        /// <response code="400">If the Guid's ModelState is invalid</response>
        /// <response code="404">If the item could not be found</response>
        /// <response code="500">If the transaction has failed</response>
        [HttpGet("{basketGuid}")]
        public IActionResult GetBasketById(Guid basketGuid)
        {
            // Errors when binding the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var repoResponse = _basket.GetBasketById(basketGuid);

            if (repoResponse.ReturnData == null && repoResponse.IsTransactionSuccess)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (!repoResponse.IsTransactionSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(repoResponse);
        }

        /// <summary>
        /// Add new basket to persistance
        /// </summary>
        /// <remarks>
        /// Creates a new empty basket that can be filled by items.
        ///  
        ///     POST /v1/basket
        /// 
        /// </remarks>
        /// <returns>Returns a reply wrapping the newly created basket.</returns>
        /// <response code="200">Returns created basket</response>
        /// <response code="500">If the basket cannot be added to the repository</response>
        [HttpPost]
        public IActionResult CreateBasket()
        {
            var repoResponse = _basket.CreateBasket();

            if (!repoResponse.IsTransactionSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(repoResponse);
        }

        /// <summary>
        /// Add item to the basked living in persistance.
        /// </summary>
        /// <remarks>
        /// The basketGuid is a path attribute, the new items is binded from the request's body.
        ///  
        ///     POST /v1/basket/{basketGuid}
        ///     {
        ///         "ItemGuid":"cf791242-ed42-11ce-bacd-00aa0057b223",
        ///         "Quantity":"1"
        ///     }
        /// 
        /// </remarks>
        /// <returns>Returns a reply wrapping the newly added item in its basket.</returns>
        /// <response code="200">Returns the newly added item in its basket</response>
        /// <response code="500">If the item cannot be added to the repository</response>
        [HttpPost("{basketGuid}")]
        public IActionResult AddBasketItem(Guid basketGuid, [FromBody]Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var repoResponse = _basket.AddBasketItem(basketGuid, item);

            if (!repoResponse.IsTransactionSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(repoResponse);
        }

        /// <summary>
        /// Deletes a basket from persistance
        /// </summary>
        /// <remarks>
        /// Deletes a basked from persistance based on it's Guid
        ///  
        ///     DELETE /v1/basket/{basketGuid}
        /// 
        /// </remarks>
        /// <returns>Returns a reply wrapping the deleted basket.</returns>
        /// <response code="200">Returns the deleted item.</response>
        /// <response code="400">If the item's ModelState is invalid</response>
        /// <response code="400">If the item cannot be found</response>
        /// <response code="500">If the item cannot be added to the repository</response>
        [HttpDelete("{basketGuid}")]
        public IActionResult DeleteBasket(Guid basketGuid)
        {
            // Errors when binding the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var repoResponse = _basket.DeleteBasket(basketGuid);

            if (repoResponse.RecordsAffected == 0 && repoResponse.IsTransactionSuccess)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (!repoResponse.IsTransactionSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(repoResponse);
        }

        /// <summary>
        /// Delete item from persistance basket.
        /// </summary>
        /// <remarks> 
        /// The basketGuid is a path attribute, the new items is binded from the request's body.
        ///  
        ///     DELETE /v1/basket/{basketGuid}
        ///     {
        ///         "ItemGuid":"cf791242-ed42-11ce-bacd-00aa0057b223",
        ///         "Quantity":"1"
        ///     }
        /// 
        /// </remarks>
        /// <returns>Returns a reply wrapping the deleted item's basket.</returns>
        /// <response code="200">Returns the deleted item's basket.</response>
        /// <response code="400">If the item's ModelState is invalid</response>
        /// <response code="400">If the item cannot be found</response>
        /// <response code="500">If the item cannot be added to the repository</response>
        [HttpDelete("{id}")]
        public IActionResult DeleteBasketItem(Guid id, [FromBody]Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var repoResponse = _basket.DeleteBasketItem(id, item);

            if (repoResponse.RecordsAffected == 0 && repoResponse.IsTransactionSuccess)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (!repoResponse.IsTransactionSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(repoResponse);
        }

        // TODO
        // PUT: Update basket on persistance
        // RepositoryReturnType<Basket> UpdateBasket(Guid basketGuid);
        // PUT: Update basket items on persistance
        // RepositoryReturnType<Basket> UpdateBasketItem(Guid basketGuid, Item item);
    }
}
