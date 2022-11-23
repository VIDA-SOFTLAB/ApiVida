namespace ApiVida.Entities
{
    public class DoctorEntity : UserEntity {
        public int doctorId { get; set; }
        public int userId { get; set; }
        public string CRM { get; set; }
        MedicalSpecialtyEntity medicalSpecialties { get; set; }

    }
}
