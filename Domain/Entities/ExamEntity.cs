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
    public class ExamEntity
    {

        [Key]
        [JsonProperty(PropertyName = "examId")]
        public string ExamId { get; set; }
		
        [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [JsonProperty(PropertyName = "specialty")]
        public string Specialty { get; set; }

        //[Required(ErrorMessage = ErrorBase.erro_camponec)]
        [JsonProperty(PropertyName = "medicalCenterName")]
        public string MedicalCenterName { get; set; }

//        [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [DataType(DataType.Date, ErrorMessage = ErrorBase.erro_for)]
        [JsonProperty(PropertyName = "examDatetime")]
        public DateTime ExamDatetime { get; set; }

  //      [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [DataType(DataType.Date, ErrorMessage = ErrorBase.erro_for)]
        [JsonProperty(PropertyName = "registerDate")]
        public DateTime RegisterDate { get; set; }

    //    [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [DataType(DataType.Date, ErrorMessage = ErrorBase.erro_for)]
        [JsonProperty(PropertyName = "lastRegisterUpdate")]
        public DateTime LastRegisterUpdate { get; set; }

        [JsonProperty(PropertyName = "patientCpf")]
        public string PatientCpf { get; set; }

    }
}
