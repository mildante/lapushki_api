using lapushki_api.Requests;
using Microsoft.AspNetCore.Mvc;

namespace lapushki_api.Interfaces
{
    public interface IAppointmentsService
    {
        Task<IActionResult> GetAllServices();
        Task<IActionResult> GetAllDoctors();
        Task<IActionResult> CreateDoctor(CreateDoctorRequest request);
        Task<IActionResult> UpdateDoctor(DoctorModel doctorModel);
        Task<IActionResult> DeleteDoctor(int doctor_id);
        Task<IActionResult> GetAllAppointments();
        Task<IActionResult> GetAllAppointmentsByUser(int user_id);
        Task<IActionResult> GetAllAppointmentsByDoctor(int doctor_id);
        Task<IActionResult> AddAppointment(AppointmentModel appointmentModel);
        Task<IActionResult> UpdateAppointment(AppointmentModel appointmentModel);
        Task<IActionResult> DeleteAppointment(int appointment_id);
    }
}
