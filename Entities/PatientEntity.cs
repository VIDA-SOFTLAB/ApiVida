namespace ApiVida.Entities
{
    public class PatientEntity: UserEntity
    {
        MedicalInsurance? MedicalInsuranceInfos { get; set; }
        public string? MedicalInsuranceId { get; set; }

    }
}
