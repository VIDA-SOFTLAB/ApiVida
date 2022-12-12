using ApiVida.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using ApiVida.Domain.Entities;

namespace ApiVida.Domain.Entities
{
    public class MedicalSpecialityEntityDTO
    {
        [Key]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [MinLength(2, ErrorMessage = ErrorBase.erro_min)]
        [MaxLength(127, ErrorMessage = ErrorBase.erro_max)]
        [JsonProperty(PropertyName = "nome")]
        public string Nome { get; set; }

        [MinLength(4, ErrorMessage = ErrorBase.erro_min)]
        [MaxLength(255, ErrorMessage = ErrorBase.erro_max)]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "doctors")]
        public virtual ICollection<DoctorEntity> Doctors { get; set; }
    }
}
