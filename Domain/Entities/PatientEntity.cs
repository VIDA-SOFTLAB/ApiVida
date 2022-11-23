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


namespace ApiVida.Entities
{
    public class PatientEntity: UserEntity
    {
        [Key]
        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("convenio")]
        public ConvenioDTO Convenio { get; set; }

        [JsonProperty("endereco")]
        public Endereco Endereco { get; set; }

        [JsonProperty("celular")]
        public string Celular { get; set; }

        [JsonProperty("dataNascimento")]
        public string DataNascimento { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_nec)]
        [JsonProperty(PropertyName = "medicalInsurance")]
        public MedicalInsuranceEntity MedicalInsurance {get; set;}

    }
}
