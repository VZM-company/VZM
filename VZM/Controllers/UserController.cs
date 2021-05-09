using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
                return Ok(user);
            }
        }
    }
}
