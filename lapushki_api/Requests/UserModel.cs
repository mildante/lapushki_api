using lapushki_api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lapushki_api.Requests
{
    public class UserModel
    {
        public int id_user { get; set; }
        public string name { get; set; }
        public string? surname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public string gender { get; set; }
        public DateOnly date_of_birth { get; set; }
        public int role_id { get; set; }
    }
}
