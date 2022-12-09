using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ApiVida.Domain.Errors;
using ApiVida.Domain.Entities;

namespace ApiVida.Domain.Entities
{
    public class ConsulEntity
    {

        [Key]
        [JsonProperty(PropertyName = "consultId")]
        public string ConsultId { get; set; }
		
        [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [JsonProperty(PropertyName = "specialty")]
        public string Specialty { get; set; }

        //[Required(ErrorMessage = ErrorBase.erro_camponec)]
        [JsonProperty(PropertyName = "medicalCenterId")]
        public string MedicalCenterId { get; set; }

//        [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [DataType(DataType.Date, ErrorMessage = ErrorBase.erro_for)]
        [JsonProperty(PropertyName = "consultDatetime")]
        public DateTime ExamDatetime { get; set; }

  //      [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [DataType(DataType.Date, ErrorMessage = ErrorBase.erro_for)]
        [JsonProperty(PropertyName = "registerDate")]
        public DateTime RegisterDate { get; set; }

    //    [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [DataType(DataType.Date, ErrorMessage = ErrorBase.erro_for)]
        [JsonProperty(PropertyName = "lastRegisterUpdate")]
        public DateTime LastRegisterUpdate { get; set; }

        [JsonProperty(PropertyName = "patientId")]
        public string PatientId { get; set; }

        [JsonProperty(PropertyName = "doctorId")]
        public string DoctorId { get; set; }

    }
}
