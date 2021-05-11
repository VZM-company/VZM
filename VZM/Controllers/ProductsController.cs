using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VZM.Data;
using VZM.Entities;
using System;
using VZM.ViewModels;
using System.Linq;
using System.Collections.Generic;

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
            if (product.ProductId != default)
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

        // POST: api/products/update
        [HttpPut]
        [Route("update")]
        public IActionResult Put(Product product)
        {
            var oldProduct = _dataManager.Products.GetProduct(product.ProductId);
            if (product.SellerId != _dataManager.AuthorizedUser.UserId || oldProduct == null)
            {
                return StatusCode(500);
            }
            else
            {
                product.CreatedAt = oldProduct.CreatedAt;
                _dataManager.Products.SaveProduct(product);
            }

            return Ok(product);
        }

        // GET: api/products/find
        [HttpGet]
        [Route("find")]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var fpvm = new FindProductViewModel
            {
                Title = HttpContext.Request.Query["title"],
                StartPrice = float.Parse(HttpContext.Request.Query["startPrice"].ToString()),
                EndPrice = float.Parse(HttpContext.Request.Query["endPrice"].ToString()),
                Category = HttpContext.Request.Query["category"],
            };

            IEnumerable<Product> products = null;
            if (fpvm.Category != "undefined")
            {
                var cat = _dataManager.Categories.GetCategoryByName(fpvm.Category);
                products = _dataManager.Products.GetProductsByCategory(cat);
            }

            products ??= _dataManager.Products.GetProducts();

            fpvm.EndPrice = fpvm.EndPrice == 0 ? int.MaxValue : fpvm.EndPrice;

            products = products.Where(x => x.Price >= fpvm.StartPrice && x.Price <= fpvm.EndPrice && x.Title.Contains(fpvm.Title));

            return Ok(products);
        }

        // GET: api/products/top
        [HttpGet]
        [Route("top", Name ="Top")]
        public ActionResult<IEnumerable<Product>> Top()
        {
            var obj = from prod in _dataManager.Products.GetProducts().Take(10)
                      let discount = _dataManager.Discounts.GetDiscount(prod.ProductId)
                      select new
                      {
                          Name = prod.Title,
                          Price = prod.Price,
                          ImageUrl = prod.ImageUrl,
                          ActualPrice = prod.Price - prod.Price * discount.Value / 100,
                          Discount = discount.Value,
                          Left = (discount.ExpiredAt - discount.CreatedAt).Days,
                          ProductId = prod.ProductId,
                      };

            return Ok(obj);
        }

    }
}
