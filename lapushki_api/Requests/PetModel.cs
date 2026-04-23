using lapushki_api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace lapushki_api.Requests
{
    public class PetModel
    {
        public int id_pet { get; set; }
        public string name { get; set; }
        public string breed { get; set; }
        public string species { get; set; }
        public string? description { get; set; }
        public string gender { get; set; }
        public string? image { get; set; }
        public DateOnly date_of_birth { get; set; }
        public int user_id { get; set; }
    }
}
