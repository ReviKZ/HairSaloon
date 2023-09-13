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
            try
            {
                return Ok(_loginUserService.Login(user));
            }
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
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

        //Get Person data
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetPerson([FromRoute] int id)
        {
            try
            {
                return Ok(_personService.GetPerson(id));
            }
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> GetPersonList()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet]
        [Route("list/hairdressers")]
        public async Task<ActionResult> GetHairDresserList()
        {
            return Ok(_userService.GetAllHairDressers());
        }


    }
}
