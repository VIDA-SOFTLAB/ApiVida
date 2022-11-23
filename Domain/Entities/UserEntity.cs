using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiVida.Entities
{
    public class UserEntity
    {

        [Key]
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }
		
        [Required(ErrorMessage = ErrorBase.erro_nec)]
        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_nec)]
        [JsonProperty(PropertyName = "cpf")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_nec)]
        [JsonProperty(PropertyName = "rg")]
        public string Rg { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_nec)]
        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_nec)]
        [DataType(DataType.Date, ErrorMessage = ErrorBase.erro_for)]
        [JsonProperty(PropertyName = "birthDate")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_nec)]
        [DataType(DataType.Date, ErrorMessage = ErrorBase.erro_for)]
        [JsonProperty(PropertyName = "registerDate")]
        public DateTime RegisterDate { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_nec)]
        [DataType(DataType.Date, ErrorMessage = ErrorBase.erro_for)]
        [JsonProperty(PropertyName = "lastRegisterUpdate")]
        public DateTime LastRegisterUpdate { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_nec)]
        [DataType(DataType.EmailAddress, ErrorMessage = ErrorBase.erro_for)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = ErrorBase.erro_for)]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_nec)]
        [JsonProperty(PropertyName = "biologicalSexEnum")]
        public BiologicalSexEnum BiologicalSexEnum {get; set;}

        [Required(ErrorMessage = ErrorBase.erro_nec)]
        [JsonProperty(PropertyName = "gender")]
        public GenderEnum GenderEnum {get; set;}

        [Required(ErrorMessage = ErrorBase.erro_nec)]
        [JsonProperty(PropertyName = "endereco")]
        public Endereco Endereco {get; set;}

    }
}
