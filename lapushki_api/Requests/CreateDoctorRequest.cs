namespace lapushki_api.Requests
{
    public class CreateDoctorRequest
    {
        public string specialization { get; set; }
        public TimeOnly work_start { get; set; }
        public TimeOnly work_end { get; set; }
        public int duration_slot { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string gender { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public DateOnly date_of_birth { get; set; }
        public bool is_active { get; set; }

    }
}
