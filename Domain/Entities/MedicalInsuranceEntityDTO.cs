using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiVida.Domain
{
    public class MedicalInsuranceEntityDTO
    {
    [Key]
        [JsonProperty(PropertyName = "enterpriseId")]
        public string EnterpriseId { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_nec)]
        [MinLength(3, ErrorMessage = ErrorBase.erro_min)]
        [MaxLength(63, ErrorMessage = ErrorBase.erro_max)]
        [JsonProperty(PropertyName = "enterpriseName")]
        public string EnterpriseName { get; set; }

        // um horario possui apenas um paciente
        [JsonProperty(PropertyName = "medicalPlans")]
        public virtual ICollection<MedicalInsurancePlanEntity> MedicalPlans { get; set; }

        // pode ter mais de local que atende
        [JsonProperty(PropertyName = "medicalCenters")]
        public virtual ICollection<MedicalCenterEntity> MedicalCenters { get; set; }
        }
}
