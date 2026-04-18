using lapushki_api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lapushki_api.Requests
{
    public class AppointmentModel
    {
        public int id { get; set; }
        public int doctor_id { get; set; }
        public int service_id { get; set; }
        public int pet_id { get; set; }
        public DateOnly date { get; set; }
        public TimeOnly time { get; set; }
    }
}
