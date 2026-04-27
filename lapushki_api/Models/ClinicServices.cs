using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace lapushki_api.Models
{
    public class ClinicServices
    {
        [Key] public int id_service { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        
        [JsonIgnore]
        public ICollection<Appointments> appointments { get; set; }

    }
}
