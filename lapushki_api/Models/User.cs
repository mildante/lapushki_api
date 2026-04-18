using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace lapushki_api.Models
{
    public class User
    {        
        [Key]
        public int id_user { get; set; }
        public string name { get; set; }
        public string? surname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public string gender { get; set; }
        public DateOnly date_of_birth { get; set; }

        [Required]
        [ForeignKey("role")]
        public int role_id { get; set; }
        public Role role { get; set; }

        [JsonIgnore]
        public ICollection<Pet> pets { get; set; }
    }
}
