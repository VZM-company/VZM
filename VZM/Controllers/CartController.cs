using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VZM.Data;
using VZM.Entities;

namespace VZM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private DataManager _dataManager;
        public CartController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        // POST: api/cart/add
        [HttpPost]
        [Route("add")]
        public IActionResult Post(Product p)
        {
            var uId = _dataManager.AuthorizedUser?.UserId;
            if ( uId != null)
            {
                try
                {
                    _dataManager.Carts.Add(p.ProductId, (Guid)uId);
                }
                catch (ArgumentException)
                {
                    return StatusCode(500);
                }              
            }

            return StatusCode(200);
        }

        // GET: api/cart/
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(ModifyProduct(_dataManager.Carts.GetItems(_dataManager.AuthorizedUser.UserId)));
        }

        // POST: api/cart/purchase/
        [HttpPost]
        [Route("purchase")]
        public IActionResult Post()
        {
            var uId = _dataManager.AuthorizedUser?.UserId;
            if (uId != null)
            {
                _dataManager.Carts.Purchase((Guid)uId);
            }

            return StatusCode(200);
        }

        private dynamic ModifyProduct(IEnumerable<Product> products)
        {
            var obj = from prod in products
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

            return obj;
        }
    }
}
