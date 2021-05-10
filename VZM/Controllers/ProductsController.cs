using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VZM.Data;
using VZM.Entities;
using System;

namespace VZM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private DataManager _dataManager;
        public ProductsController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        // POST: api/products/create
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> Post(Product product)
        {
            if(_dataManager.AuthorizedUser?.UserId != null)
            {
                //product.SellerId = _dataManager.AuthorizedUser.UserId;
                product.CreatedAt = DateTime.Now;
                _dataManager.Products.SaveProduct(product);
                return Ok(product);
            }

            return StatusCode(500);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var product = _dataManager.Products.GetProduct(id);
            if (product.ProductId == default)
            {
                _dataManager.Products.DeleteProduct(id);
            }

            return Ok(product);
        }

        // GET: api/products/{id}
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(Guid id)
        {
            var product = _dataManager.Products.GetProduct(id);
            return Ok(product);
        }
    }
}
