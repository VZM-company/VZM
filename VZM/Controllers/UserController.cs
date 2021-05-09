﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> Registration(User user)
        {
            try
            {
                _dataManager.Users.SaveUser(user);
                return CreatedAtAction(nameof(Registration), user);
            }
            catch
            {
                return BadRequest();
            }
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
