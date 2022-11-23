namespace ApiVida.Entities
{
    public class MedicalInsurance : PatientEntity
    {
        public string EnterpriseName { get; set; }
        public int EnterpriseId { get; set; }
        public string PlanName { get; set; }

    }
}
