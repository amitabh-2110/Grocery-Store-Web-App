using BusinessLogicLayer.LogicServices.TokenLogicService;
using BusinessLogicLayer.LogicServices.UserAuthLogicService;
using BusinessObjectLayer.DatabaseEntities;
using BusinessObjectLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Grocery_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController: Controller
    {
        private readonly IUserAuthLogic _userAuthLogic;
        private readonly ITokenLogic _tokenLogic;

        public UserAuthController(IUserAuthLogic userAuthLogic, ITokenLogic tokenLogic)
        {
            _userAuthLogic = userAuthLogic;
            _tokenLogic = tokenLogic;
        }

        [HttpPost]
        [Route("signup"), AllowAnonymous]
        public async Task<ActionResult> Signup([FromForm] RegisteredPerson person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isRegistered = await _userAuthLogic.IsRegistered(person.Email);

            if (isRegistered)
                return BadRequest("User already registerd");

            await _userAuthLogic.RegisterUser(person);

            ResponseModel res = new ResponseModel
            {
                status = "ok",
                title = "Registration Successfull",
            };

            return Ok(new { res.status, res.title });
        }

        [HttpPost]
        [Route("login"), AllowAnonymous]
        public async Task<ActionResult> Login([FromForm] Login person)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isRegistered = await _userAuthLogic.AuthenticateUser(person.Email, person.Password);

            if (!isRegistered)
            {
                ResponseModel failedRes = new ResponseModel
                {
                    status = "failed",
                    title = "Wrong Credentials"
                };

                return BadRequest(new { failedRes.status, failedRes.title });
            }

            string role = await _userAuthLogic.FetchUserRole(person.Email);
            var token = _tokenLogic.CreateToken(person.Email, role);

            ResponseModel res = new ResponseModel
            {
                status = "ok",
                title = "Login Successfull",
                token = token
            };

            return Ok(new { res.status, res.title, res.token });
        }

        [HttpGet]
        [Route("fetch-user"), Authorize]
        public async Task<ActionResult<object>> FetchUser()
        {
            string userId = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userAuthLogic.FetchUser(userId);

            return Ok(new { user.Email, user.FullName, user.Role });
        }
    }
}
