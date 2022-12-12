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
        [JsonProperty("cpf")]
        public string Cpf { get; set; }

         [JsonProperty(PropertyName = "idAdministrador")]
        public string IdAdministrador { get; set; }
        

       // [Required(ErrorMessage = ErrorBase.erro_camponec)]
    //    [JsonProperty(PropertyName = "medicalInsurance")]
   //     public MedicalInsuranceEntity MedicalInsurance {get; set;}

    }
}
