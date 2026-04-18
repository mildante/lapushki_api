using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lapushki_api.Models
{
    public class Appointments
    {
        [Key] public int id { get; set; }

        [Required]
        [ForeignKey("doctor")]
        public int doctor_id { get; set; }
        public Doctor doctor { get; set; }

        [Required]
        [ForeignKey("clinic_service")]
        public int service_id { get; set; }
        public ClinicServices clinic_service { get; set; }

        [Required]
        [ForeignKey("pet")]
        public int pet_id { get; set; }
        public Pet pet { get; set; }

        public DateOnly date { get; set; }
        public TimeOnly time {  get; set;}
        public string status { get; set; } = "Pending";
        public string? payment_id { get; set; }
    }
}
