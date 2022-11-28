using System.Text.Json.Serialization;
namespace ApiVida.Entities
{
    public class DoctorEntity : UserEntity {

        [JsonProperty(PropertyName = "doctorId")]
        public string DoctorId { get; set; }

        [JsonProperty(PropertyName = "idMedicalSpeciality")]
        public string IdMedicalSpeciality { get; set; }

        [JsonProperty(PropertyName = "crm")]
        public string CRM { get; set; }

        // o profissional possui uma lista de agendamentos
        [JsonProperty(PropertyName = "scheduling")]
        public virtual ICollection<SchedulingEntity> Scheduling { get; set; }

        // pode ter mais de um profissional por serviço
        [JsonProperty(PropertyName = "medicalSpecialties")]
        public virtual ICollection<MedicalSpecialityEntity> MedicalSpecialties { get; set; }

    }
}
