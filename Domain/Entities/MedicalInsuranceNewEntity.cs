using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiVida.Domain.Entities
{
    public class MedicalInsuranceNewEntity
    {
        [Key]
        [JsonProperty(PropertyName = "medicalInsuranceId")]
        public string MedicalInsuranceId { get; set; }

        [JsonProperty(PropertyName = "medicalInsuranceName")]
        public string MedicalInsuranceName { get; set; }

    }
}
