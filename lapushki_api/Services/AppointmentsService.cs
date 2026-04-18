using lapushki_api.Data;
using lapushki_api.Interfaces;
using lapushki_api.Models;
using lapushki_api.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lapushki_api.Services
{
    public class AppointmentsService : IAppointmentsService
    {
        private readonly ContextDb _ContextDb;

        public AppointmentsService(ContextDb ContextDb)
        {
            _ContextDb = ContextDb;
        }
        public async Task<IActionResult> GetAllServices()
        {
            var list = await _ContextDb.ClinicServices.ToListAsync();
            return new OkObjectResult(new { status = true, list });
        }

        public async Task<IActionResult> GetAllDoctors()
        {
            var list = await _ContextDb.Doctors.Include(x=>x.user).ToListAsync();
            return new OkObjectResult(new { status = true, list });
        }
        public async Task<IActionResult> GetAllDoctorServices()
        {
            var list = await _ContextDb.DoctorServices.Include(x=>x.clinic_service).Include(x=>x.doctor).ThenInclude(x=>x.user).ToListAsync();
            return new OkObjectResult(new { status = true, list });
        }

        public async Task<IActionResult> GetAllAppointments()
        {
            var list = await _ContextDb.Appointments.ToListAsync();
            return new OkObjectResult(new { status = true, list });
        }

        public async Task<IActionResult> GetAllAppointmentsByUser(int user_id)
        {
            var list = await _ContextDb.Appointments.Include(x=>x.pet).Where(x=>x.pet.user_id == user_id).ToListAsync();
            return new OkObjectResult(new { status = true, list });
        }

        public async Task<IActionResult> GetAllAppointmentsByDoctor(int doctor_id)
        {
            var list = await _ContextDb.Appointments.Where(x => x.doctor_id == doctor_id).ToListAsync();
            return new OkObjectResult(new { status = true, list });
        }

        public async Task<IActionResult> AddAppointment(AppointmentModel appointmentModel)
        {
            var newAppointment = new Appointments()
            {
                doctor_id = appointmentModel.doctor_id,
                service_id = appointmentModel.service_id,
                pet_id = appointmentModel.pet_id,
                date = appointmentModel.date,
                time = appointmentModel.time,
            };

            await _ContextDb.AddAsync(newAppointment);
            await _ContextDb.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Запись зарегистрирован"
            });
        }

        public async Task<IActionResult> UpdateAppointment(AppointmentModel appointmentModel)
        {
            var appointment = await _ContextDb.Appointments.FirstOrDefaultAsync(x => x.id == appointmentModel.id);
            if (appointment == null)
            {
                return new OkObjectResult(new
                {
                    status = false
                });
            }

            appointment.doctor_id = appointmentModel.doctor_id;
            appointment.service_id = appointmentModel.service_id;
            appointment.pet_id = appointmentModel.pet_id;
            appointment.date = appointmentModel.date;
            appointment.time = appointmentModel.time;

            await _ContextDb.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Запись обновлена"
            });
        }

        public async Task<IActionResult> DeleteAppointment(int appointment_id)
        {
            var appointment = await _ContextDb.Appointments.FirstOrDefaultAsync(x => x.id == appointment_id);
            if (appointment == null)
            {
                return new OkObjectResult(new
                {
                    status = false
                });
            }

            _ContextDb.Remove(appointment);
            await _ContextDb.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Запись удалена"
            });
        }

    }
}
