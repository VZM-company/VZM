using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        // GET: api/User/products/{id}
        [HttpGet("id")]
        [Route("products")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Product>> Get(string id)
        {

            var user = _dataManager.Users.GetUser(Guid.Parse(id));
            if(user == null)
            {
                return NotFound();
            }
            
            var role = _dataManager.Roles.GetRoleById(user.RoleId.ToString());
            if(role.Name == "company")
            {
                return Ok(_dataManager.Products.GetProductsBySeller(user));
            }
            else
            {
                return Ok(_dataManager.Products.GetProductsByUser(user));
            }
        }
    }
}
