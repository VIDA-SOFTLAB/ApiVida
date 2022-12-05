using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ApiVida.Domain
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
        [JsonProperty(PropertyName = "agendamento")]
        public virtual ICollection<Agendamento> Agendamento { get; set; }

        // pode ter mais de um profissional por serviço
        [JsonProperty(PropertyName = "especialidade")]
        public virtual ICollection<Especialidade> Especialidade { get; set; }

    }
}
