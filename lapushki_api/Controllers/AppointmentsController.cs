using lapushki_api.Interfaces;
using lapushki_api.Requests;
using lapushki_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace lapushki_api.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentsService _appointmentsService;
        public AppointmentsController(IAppointmentsService appointmentsService)
        {
            _appointmentsService = appointmentsService;
        }

        [HttpGet]
        [Route("getAllServices")]
        public async Task<IActionResult> GetAllServices()
        {
            return await _appointmentsService.GetAllServices();
        }

        [HttpGet]
        [Route("getAllDoctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            return await _appointmentsService.GetAllDoctors();
        }

        [HttpGet]
        [Route("getAllDoctorServices")]
        public async Task<IActionResult> GetAllDoctorServices()
        {
            return await _appointmentsService.GetAllDoctorServices();
        }

        [HttpGet]
        [Route("getAllAppointments")]
        public async Task<IActionResult> GetAllAppointments()
        {
            return await _appointmentsService.GetAllAppointments();
        }

        [HttpGet]
        [Route("getAllAppointmentsByUser")]
        public async Task<IActionResult> GetAllAppointmentsByUser(int user_id)
        {
            return await _appointmentsService.GetAllAppointmentsByUser(user_id);
        }

        [HttpGet]
        [Route("getAllAppointmentsByDoctor")]
        public async Task<IActionResult> GetAllAppointmentsByDoctor(int doctor_id)
        {
            return await _appointmentsService.GetAllAppointmentsByDoctor(doctor_id);
        }

        [HttpPost]
        [Route("addAppointment")]
        public async Task<IActionResult> AddAppointment(AppointmentModel appointmentModel)
        {
            return await _appointmentsService.AddAppointment(appointmentModel);
        }

        [HttpPut]
        [Route("updateAppointment")]
        public async Task<IActionResult> UpdateAppointment(AppointmentModel appointmentModel)
        {
            return await _appointmentsService.UpdateAppointment(appointmentModel);
        }

        [HttpDelete]
        [Route("deleteAppointment")]
        public async Task<IActionResult> DeleteAppointment(int appointmentId)
        {
            return await _appointmentsService.DeleteAppointment(appointmentId);
        }
    }
}
