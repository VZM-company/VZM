using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VZM.Data;
using VZM.Entities;

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> Login(string userName, string password)
        {
            var user = _dataManager.Users.GetUserByUsername(userName);
            if (user == null || user.PasswordHash != password)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }
    }
}
