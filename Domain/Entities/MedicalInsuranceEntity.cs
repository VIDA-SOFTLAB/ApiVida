using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ApiVida.Domain.Errors;

namespace ApiVida.Domain.Entities
{
    public class MedicalInsuranceEntity
    {
    
        [Key]
        [JsonProperty(PropertyName = "enterpriseId")]
        public string EnterpriseId { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [MinLength(3, ErrorMessage = ErrorBase.erro_min)]
        [MaxLength(63, ErrorMessage = ErrorBase.erro_max)]
        [JsonProperty(PropertyName = "enterpriseName")]
        public string EnterpriseName { get; set; }
        public List<string> CenterIds { get; set; }
        // um horario possui apenas um paciente
        [JsonProperty(PropertyName = "medicalPlans")]
        public virtual ICollection<MedicalInsurancePlanEntity> MedicalPlans { get; set; }

      
    }

}
