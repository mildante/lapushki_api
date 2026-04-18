using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace lapushki_api.Models
{
    public class ClinicServices
    {
        [Key] public int id_service { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public int duration { get; set; }

        [JsonIgnore]
        public ICollection<DoctorService> doctor_services { get; set; }
        
        [JsonIgnore]
        public ICollection<Appointments> appointments { get; set; }

    }
}
