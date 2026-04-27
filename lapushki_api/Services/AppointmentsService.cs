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

        public async Task<IActionResult> CreateDoctor(CreateDoctorRequest request)
        {
            if (request == null)
            {
                return new BadRequestObjectResult(new { status = false, message = "request == null" });
            }

            var newUser = new User
            {
                name = request.name,
                surname = request.surname,
                gender = request.gender,
                phone = request.phone,
                email = request.email,
                password = "123",
                role_id = 2,
                avatar = "http://localhost:5276/images/default-avatar.png",
                date_of_birth = request.date_of_birth,
            };

            await _ContextDb.Users.AddAsync(newUser);
            await _ContextDb.SaveChangesAsync();

            var newDoctor = new Doctor
            {
                id_user = newUser.id_user,
                specialization = request.specialization,
                work_start = request.work_start,
                work_end = request.work_end,
                duration_slot = request.duration_slot,
                is_active = request.is_active,
            };

            await _ContextDb.Doctors.AddAsync(newDoctor);
            await _ContextDb.SaveChangesAsync();

            return new OkObjectResult(new { status = true, message = "Доктор создан" });
        }
        public async Task<IActionResult> UpdateDoctor(DoctorModel doctorModel)
        {
            var doctor = await _ContextDb.Doctors.Include(x=>x.user).FirstOrDefaultAsync(x => x.id_doctor == doctorModel.id_doctor);

            if (doctor == null)
            {
                return new OkObjectResult(new
                {
                    status = false
                });
            }

            doctor.user.name = doctorModel.user.name;
            doctor.user.surname = doctorModel.user.surname;
            doctor.user.phone = doctorModel.user.phone;
            doctor.user.email = doctorModel.user.email;
            doctor.user.avatar = doctorModel.user.avatar;
            doctor.user.date_of_birth = doctorModel.user.date_of_birth;
            doctor.specialization = doctorModel.specialization;
            doctor.work_start = doctorModel.work_start;
            doctor.work_end = doctorModel.work_end;
            doctor.duration_slot = doctorModel.duration_slot;
            doctor.is_active = doctorModel.is_active;

            await _ContextDb.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Запись обновлена"
            });

        }

        public async Task<IActionResult> DeleteDoctor(int doctor_id)
        {
            var doctor = await _ContextDb.Doctors.Include(x => x.user).FirstOrDefaultAsync(x => x.id_doctor == doctor_id);

            if (doctor == null)
            {
                return new OkObjectResult(new
                {
                    status = false
                });
            }

            _ContextDb.Remove(doctor);
            await _ContextDb.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Запись удален"
            });

        }
        public async Task<IActionResult> GetAllAppointments()
        {
            var list = await _ContextDb.Appointments.Include(x => x.clinic_service).Include(x => x.doctor).ThenInclude(x => x.user).Include(x => x.pet).ToListAsync();
            return new OkObjectResult(new { status = true, list });
        }

        public async Task<IActionResult> GetAllAppointmentsByUser(int user_id)
        {
            var list = await _ContextDb.Appointments.Include(x=>x.clinic_service).Include(x=>x.doctor).ThenInclude(x=>x.user).Include(x=>x.pet).Where(x=>x.pet.user_id == user_id).ToListAsync();
            return new OkObjectResult(new { status = true, list });
        }

        public async Task<IActionResult> GetAllAppointmentsByDoctor(int doctor_id)
        {
            var list = await _ContextDb.Appointments.Include(x => x.clinic_service).Include(x => x.doctor).ThenInclude(x => x.user).Include(x => x.pet).Where(x => x.doctor_id == doctor_id).ToListAsync();
            return new OkObjectResult(new { status = true, list });
        }

        public async Task<IActionResult> AddAppointment(AppointmentModel appointmentModel)
        {
            try
            {
                var newAppointment = new Appointments()
            {
                doctor_id = appointmentModel.doctor_id,
                service_id = appointmentModel.service_id,
                pet_id = appointmentModel.pet_id,
                date = appointmentModel.date,
                time = appointmentModel.time,
                status = "Pending",

            };

            await _ContextDb.AddAsync(newAppointment);
            await _ContextDb.SaveChangesAsync();

                return new OkObjectResult(new
                {
                    status = true,
                    message = "Запись зарегистрирована",
                    newAppointment
                });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
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
