namespace ApiVida.Entities
{
    public class DoctorEntity : UserEntity { 
        public int userId { get; set; }
        public int CRM { get; set; }
        public int medicalSpecialtyId { get; set; }
        public int medicalSpecialtyName { get; set; }

    }
}
