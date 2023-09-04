using HairSaloonAPI.Interfaces.DTOs;
using HairSaloonAPI.Interfaces.Services;
using HairSaloonAPI.Models.DTOs;
using HairSaloonAPI.Models.DTOs.ControllerDTOs;
using HairSaloonAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HairSaloonAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private IRegisterUserService _registerUserService;
        private ILoginUserService _loginUserService;
        private IUserService _userService;
        private IPersonService _personService;

        public UserApiController(IRegisterUserService registerUserService, ILoginUserService loginUserService, IUserService userService, IPersonService personService)
        {
            _registerUserService = registerUserService;
            _loginUserService = loginUserService;
            _userService = userService;
            _personService = personService;
        }

        //Register & Create Person

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<string>> Register(RegisterDTO requestBody)
        {
            try
            {
                _registerUserService.CreateUser(requestBody.user);
                int id = _userService.GetLastUserId();
                _personService.CreatePerson(id, requestBody.gender, requestBody.personType, requestBody.person);
                return Ok("User & Person has been created");

            }
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
