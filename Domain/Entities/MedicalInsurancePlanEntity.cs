using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVida.Entities
{
    public class MedicalInsurancePlanEntity
    {

        [Key]
        [JsonProperty(PropertyName = "planId")]
        public string PlanId { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_nec)]
        [MinLength(3, ErrorMessage = ErrorBase.erro_min)]
        [MaxLength(63, ErrorMessage = ErrorBase.erro_max)]
        [JsonProperty(PropertyName = "PlanName")]
        public string PlanName { get; set; }

        [JsonProperty(PropertyName = "enterpriseId")]
        public virtual string EnterpriseId { get; set; }

    }
}
