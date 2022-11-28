namespace ApiVida.Entities
{
    public class MedicalSpecialityEntity{
        
        [Key]
        [JsonProperty(PropertyName = "medicalSpecialtyId")]
        public string MedicalSpecialtyId { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_nec)]
        [MinLength(2, ErrorMessage = ErrorBase.erro_min)]
        [MaxLength(127, ErrorMessage = ErrorBase.erro_max)]
        [JsonProperty(PropertyName = "medicalSpecialtyName")]
        public string MedicalSpecialtyName { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_nec)]
        [MinLength(4, ErrorMessage = ErrorBase.erro_min)]
        [MaxLength(255, ErrorMessage = ErrorBase.erro_max)]
        [JsonProperty(PropertyName = "descricao")]
        public string Descricao { get; set; }

        [JsonProperty(PropertyName = "doctorsEspeciality")]
        public virtual ICollection<DoctorEntity> doctorsEspeciality { get; set; }

    }
}
