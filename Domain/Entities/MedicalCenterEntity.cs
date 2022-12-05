using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ApiVida.Domain.Errors;

namespace ApiVida.Domain.Entities
{
    public class MedicalCenterEntity
    {
        [Key]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [MinLength(3, ErrorMessage = ErrorBase.erro_min)]
        [MaxLength(63, ErrorMessage = ErrorBase.erro_max)]
        [JsonProperty(PropertyName = "centerName")]
        public string CenterName { get; set; }

        [JsonProperty(PropertyName = "centerAdress")]
        public AdressEntity CenterAdress { get; set; }

       [JsonProperty(PropertyName = "medicalSpecialty")]
        public virtual ICollection<MedicalSpecialityEntity> MedicalSpecialty { get; set; }
    }
}
