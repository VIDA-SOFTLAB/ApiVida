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
    public class UserEntity
    {

        [Key]
        [JsonProperty(PropertyName = "userId")]
        [JsonIgnore]
        public string UserId { get; set; }
		
        [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [JsonProperty(PropertyName = "rg")]
        public string Rg { get; set; }

       // [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }

//        [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [DataType(DataType.Date, ErrorMessage = ErrorBase.erro_for)]
        [JsonProperty(PropertyName = "birthDate")]
        public DateTime BirthDate { get; set; }

  //      [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [DataType(DataType.Date, ErrorMessage = ErrorBase.erro_for)]
        [JsonProperty(PropertyName = "registerDate")]
        public DateTime RegisterDate { get; set; }

    //    [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [DataType(DataType.Date, ErrorMessage = ErrorBase.erro_for)]
        [JsonProperty(PropertyName = "lastRegisterUpdate")]
        public DateTime LastRegisterUpdate { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [DataType(DataType.EmailAddress, ErrorMessage = ErrorBase.erro_for)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = ErrorBase.erro_for)]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

//        [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [JsonProperty(PropertyName = "biologicalSexEnum")]
        public BiologicalSexEnum BiologicalSexEnum {get; set;}

      //  [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [JsonProperty(PropertyName = "gender")]
        public GenderEnum GenderEnum {get; set;}

//        [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [JsonProperty(PropertyName = "adress")]
        public AdressEntity Adress {get; set;}

    }
}
