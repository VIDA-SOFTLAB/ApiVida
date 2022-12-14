using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVida.Domain.Errors;

namespace ApiVida.Domain.Entities
{

    public class AdmEntity : UserEntity
    {

        [Key]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }


        [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [MinLength(8, ErrorMessage = ErrorBase.erro_min)]
        [MaxLength(64, ErrorMessage = ErrorBase.erro_max)]
        [DataType(DataType.EmailAddress, ErrorMessage = ErrorBase.erro_for)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = ErrorBase.erro_for)]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [JsonProperty(PropertyName = "password")]
        [MinLength(8, ErrorMessage = ErrorBase.erro_min)]
        public string Password { get; set; }

        
        [JsonProperty(PropertyName = "patient")]
        public virtual ICollection<PatientEntity> Patient { get; set; }

/*        [JsonProperty(PropertyName = "doctorEntity")]
        public virtual ICollection<DoctorEntity> DoctorEntity { get; set; }
*/
        // Hashear Password
        public void HashearPassword()
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(this.Password + ErrorBase.global_salt));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                this.Password = builder.ToString();
            }
        }
    }
}

