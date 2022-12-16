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
    public class MedicalCenterNewEntity
    {
        [Key]
        [JsonProperty(PropertyName = "medicalCenterId")]
        public string MedicalCenterId { get; set; }

        [JsonProperty(PropertyName = "medicalCenterName")]
        public string MedicalCenterName { get; set; }

    }
}
