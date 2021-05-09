using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using VZM.Data;
using VZM.Entities;
using VZM.ViewModels;

namespace VZM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private DataManager _dataManager;

        public UserController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        // POST: api/user/register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> Post(RegistrationViewModel userArg)
        {
            var user = _dataManager.Users.GetUserByUsername(userArg.UserName);
            if (user != null)
            {
                return Ok(null);
            }

            var role = _dataManager.Roles.GetRoleByTitle(userArg.Role);
            if(role == null || userArg.Name == "" || userArg.PasswordHash == "" || userArg.UserName == "")
            {
                return BadRequest();
            }

            var newUser = new User()
            {
                Name = userArg.Name,
                UserName = userArg.UserName,
                PasswordHash = userArg.PasswordHash,
                Email = userArg.Email,
                RoleId = role.RoleId,
                Role = role,
                CreatedAt = DateTime.Now,
                Info = "Add your personal info",
            };

            _dataManager.Users.SaveUser(newUser);
            return Ok(newUser);
        }

        // POST: api/user/login
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> Post(LoginViewModel loginViewModel)
        {
            var user = _dataManager.Users.GetUserByUsername(loginViewModel.UserName);
            if (user == null || user.PasswordHash != loginViewModel.Password)
            {
                return Ok(null);
            }
            else
            {
                _dataManager.AuthorizedUser.UserId = user.UserId;
                return Ok(user);
            }
        }

        // GET: api/user/products/{id}
        [HttpGet("id")]
        [Route("products")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Product>> Get(User u)
        {

            var user = _dataManager.Users.GetUser(u.UserId);
            if(user == null)
            {
                return NotFound();
            }
            
            var role = _dataManager.Roles.GetRoleById((Guid)user.RoleId);
            if (role.Name == "company")
            {
                var obj = from prod in _dataManager.Products.GetProductsBySeller(user)
                          let discount = _dataManager.Discounts.GetDiscount(prod.ProductId)
                          select new
                          {
                              Name = prod.Title,
                              Price = prod.Price,
                              ImageUrl = prod.ImageUrl,
                              Discount = discount.Value,
                              Left = (discount.ExpiredAt - discount.CreatedAt),
                          };

                return Ok(obj);
            }
            else
            {
                var obj = from prod in _dataManager.Products.GetProductsByUser(user)
                          let discount = _dataManager.Discounts.GetDiscount(prod.ProductId)
                          select new
                          {
                              Name = prod.Title,
                              Price = prod.Price,
                              ImageUrl = prod.ImageUrl,
                              Discount = discount.Value,
                              Left = (discount.ExpiredAt - discount.CreatedAt),
                          };

                return Ok(obj);
            }
        }
    }
}
