using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace lapushki_api.Models
{
    public class DoctorService
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
    }
}
