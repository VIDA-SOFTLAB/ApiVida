namespace ApiVida.Entities
{
    public class MedicalInsuranceEntity
    {
        public string EnterpriseName { get; set; }
        public int EnterpriseId { get; set; }
        MedicalInsurancePlanEntity MedicalPlans { get; set; }
    }
}
