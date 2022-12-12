using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ApiVida.Domain.Errors;

namespace ApiVida.Domain.Entities
{
    public class DoctorEntityDTO
    {
        [Key]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [MinLength(2, ErrorMessage = ErrorBase.erro_min)]
        [MaxLength(128, ErrorMessage = ErrorBase.erro_max)]
        [JsonProperty(PropertyName = "nomeCompleto")]
        public string NomeCompleto { get; set; }

        [MinLength(8, ErrorMessage = ErrorBase.erro_min)]
        [MaxLength(64, ErrorMessage = ErrorBase.erro_max)]
        [DataType(DataType.EmailAddress, ErrorMessage = ErrorBase.erro_for)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = ErrorBase.erro_for)]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "pin")]
        public string Pin { get; set; }

        [JsonProperty(PropertyName = "idEspecialidade")]
        public string IdEspecialidade { get; set; }

        // o profissional possui uma lista de agendamentos
        [JsonProperty(PropertyName = "scheduling")]
        public virtual ICollection<SchedulingEntity> Scheduling { get; set; }

        // pode ter mais de um profissional por serviço
        [JsonProperty(PropertyName = "medicalSpeciality")]
        public virtual ICollection<MedicalSpecialityEntity> MedicalSpeciality { get; set; }

    }
}
