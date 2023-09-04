using HairSaloonAPI.Interfaces.Services;
using HairSaloonAPI.Models.DTOs;
using HairSaloonAPI.Models.DTOs.ControllerDTOs;
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

        // Delete User & Person

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                _personService.DeletePerson(id);
                _userService.DeleteUser(id);

                return Ok("User & Person has been deleted");
            }
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        //Login
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> Login(LoginUserDTO user)
        {
            if (!_loginUserService.CheckIfUsernameExist(user.UserName))
            {
                return NotFound("We haven't found a user with this username");
            }

            if (!_loginUserService.VerifyPasswordHash(user.Password, user.UserName))
            {
                return ValidationProblem("The password is incorrect");
            }

            return Ok("You have been logged in");
        }

        //Edit Person data
        [HttpPut]
        [Route("edit/{id}")]
        public async Task<ActionResult<string>> Edit([FromRoute] int id, [FromBody] PersonDTO person)
        {
            try
            {
                _personService.EditPerson(id, person);

                return Ok("The person has been edited");
            }
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
