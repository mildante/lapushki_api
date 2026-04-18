using lapushki_api.Models;
using Microsoft.EntityFrameworkCore;

namespace lapushki_api.Data
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<ClinicServices> ClinicServices { get; set; }
        public DbSet<DoctorService> DoctorServices { get; set; }
        public DbSet<Appointments> Appointments { get; set; }
    }
}
