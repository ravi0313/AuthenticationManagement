namespace AuthenticationManagement.API
{
    public class Patient
    {
        public int Id { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string Prescription { get; set; }
        public AppUser AppUser { get; set; }
    }
}
