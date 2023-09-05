using HairSaloonAPI.Interfaces.Services;
using HairSaloonAPI.Models.DTOs;
using HairSaloonAPI.Models.DTOs.ControllerDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HairSaloonAPI.Controllers
{
    [Route("api/appointment")]
    [ApiController]
    public class AppointmentApiController : ControllerBase
    {
        private ILoginUserService _loginUserService;
        private IUserService _userService;
        private IAppointmentService _appointmentService;

        public AppointmentApiController(ILoginUserService loginUserService, IUserService userService, IAppointmentService appointmentService)
        {
            _loginUserService = loginUserService;
            _userService = userService;
            _appointmentService = appointmentService;
        }

        //Create Appointment
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<string>> Create(CreateAppointmentDTO appointment)
        {
            try
            {
                _appointmentService.CreateAppointment(appointment);

                return Ok("Appointment successfully created");
            }
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        //Delete Appointment
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                _appointmentService.DeleteAppointment(id);
                return Ok("Appointment has been deleted");
            }
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        //Edit Appointment
        [HttpPut]
        [Route("edit/{id}")]
        public async Task<ActionResult<string>> Edit([FromRoute] int id, [FromBody] CreateAppointmentDTO appointment)
        {
            try
            {
                _appointmentService.UpdateAppointment(id, appointment);

                return Ok("Appointment has been updated");
            }
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        //List Appointments by User Id
        [HttpGet]
        [Route("list/{id}")]
        public async Task<ActionResult> ListByUserId([FromRoute]int id)
        {
            try
            {
                return Ok(_appointmentService.GetAppointmentListByUserId(id));
            }
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        //List Single Appointment
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetAppointment([FromRoute] int id)
        {
            try
            {
                return Ok(_appointmentService.GetAppointment(id));
            }
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPatch]
        [Route("{id}/verify")]
        public async Task<ActionResult<string>> VerfiyAppointment([FromRoute] int id)
        {
            try
            {
                _appointmentService.VerifyAppointment(id);
                return Ok("Appointment has been verified");
            }
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
