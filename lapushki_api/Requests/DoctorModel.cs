using lapushki_api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lapushki_api.Requests
{
    public class DoctorModel
    {
        public int id_doctor { get; set; }
        public string specialization { get; set; }
        public TimeOnly work_start { get; set; }
        public TimeOnly work_end { get; set; }
        public int duration_slot { get; set; }
        public bool is_active { get; set; }
        public int id_user { get; set; }
        public User user { get; set; }
    }
}
