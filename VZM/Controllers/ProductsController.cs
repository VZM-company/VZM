using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VZM.Data;
using VZM.Entities;

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

        [HttpGet]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public string Get(Product product)
        {
            return "STRING!!!";
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
                product.SellerId = _dataManager.AuthorizedUser.UserId;
                _dataManager.Products.SaveProduct(product);
                return Ok(product);
            }

            return StatusCode(500);
        } 
    }
}
