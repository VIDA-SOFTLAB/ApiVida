using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ApiVida.Domain.Errors;


namespace ApiVida.Domain.Entities
{
    public class PatientEntity: UserEntity
    {
        [Key]
        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_nec)]
        [JsonProperty(PropertyName = "medicalInsurance")]
        public MedicalInsuranceEntity MedicalInsurance {get; set;}

    }
}
