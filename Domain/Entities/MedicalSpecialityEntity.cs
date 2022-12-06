using ApiVida.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace ApiVida.Domain.Entities
{
    public class MedicalSpecialityEntity{
        
        [Key]
        [JsonProperty(PropertyName = "medicalSpecialtyId")]
        public string MedicalSpecialtyId { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [MinLength(2, ErrorMessage = ErrorBase.erro_min)]
        [MaxLength(127, ErrorMessage = ErrorBase.erro_max)]
        [JsonProperty(PropertyName = "medicalSpecialtyName")]
        public string MedicalSpecialtyName { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [MinLength(4, ErrorMessage = ErrorBase.erro_min)]
        [MaxLength(255, ErrorMessage = ErrorBase.erro_max)]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "doctorsEspeciality")]
        public virtual ICollection<DoctorEntity> doctorsEspeciality { get; set; }

    }
}
