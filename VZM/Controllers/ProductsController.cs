using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        // GET api/<ProductController>/{guid}
        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Product>> Get(string id)
        {
            var user = _dataManager.Users.GetUser(Guid.Parse(id)); //
            var products = _dataManager.Products.GetProductsByUser(user);
            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        // GET api/<ProductController>/product/{guid}
        [HttpGet("id")]
        [Route("product")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> Get(string id)
        {
            var product = _dataManager.Products.GetProduct(Guid.Parse(id));
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
