using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ApiVida.Domain.Errors;

namespace ApiVida.Domain.Entities
{
    public class SchedulingEntity
    {
        [Key]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_nec)]
        [DataType(DataType.Date, ErrorMessage = ErrorBase.erro_for)]
        [JsonProperty(PropertyName = "dateComplet")]
        public DateTime DateComplet { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_nec)]
        [DataType(DataType.DateTime, ErrorMessage = ErrorBase.erro_for)]
        [JsonProperty(PropertyName = "hour")]
        public DateTime hour { get; set; }

        [JsonProperty(PropertyName = "idDoctor")]
        public string IdDoctor { get; set; }

        // um horario possui apenas um paciente
        [JsonProperty(PropertyName = "patients")]
        public virtual ICollection<PatientEntity> Patients { get; set; }

    }
}
