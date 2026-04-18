using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace lapushki_api.Models
{
    public class Doctor
    {
        [Key]
        public int id_doctor { get; set; }
        public string specialization { get; set; }
        public TimeOnly work_start { get; set; }
        public TimeOnly work_end { get; set; }
        public int duration_slot { get; set; }
        public bool is_active { get; set; }

        [Required]
        [ForeignKey("user")]
        public int id_user { get; set; }
        public User user { get; set; }

        [JsonIgnore]
        public ICollection<Appointments> appointments { get; set; }

        [JsonIgnore]
        public ICollection<DoctorService> doctor_services { get; set; }


    }
}
