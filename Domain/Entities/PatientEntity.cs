namespace ApiVida.Entities
{
    public class PatientEntity: UserEntity
    {
        public int patientId { get; set; }
        MedicalInsurancePlanEntity? MedicalInsuranceInfos { get; set; }
        public string? MedicalInsurancePatientCode { get; set; }

    }
}
