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

        //TODO: Delete Appointment

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

        //TODO: List Appointments by User Id

        //TODO: List Single Appointment
    }
}
