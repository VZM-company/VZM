using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VZM.Data;
using VZM.Entities;

namespace VZM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private DataManager _dataManager;
        public ProductsController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        // POST: api/products/create
        [HttpGet]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> Post(Product product)
        {
            if(_dataManager.AuthorizedUser?.UserId != null)
            {
                product.SellerId = _dataManager.AuthorizedUser.UserId;
                _dataManager.Products.SaveProduct(product);
                return Ok(product);
            }

            return BadRequest();
        } 
    }
}
