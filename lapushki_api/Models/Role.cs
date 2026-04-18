using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace lapushki_api.Models
{
    public class Role
    {
        [Key] public int id_role { get; set; }
        public string name { get; set; }

        [JsonIgnore]
        public ICollection<User> users { get; set; }

    }
}
