using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VZM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VZM.Entities;

namespace VZM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
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
                OkResult()
            }
        }
    }
}
