using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Athenas.Domain
{

    public class AdmEntity : UserEntity
    {
        [Required(ErrorMessage = ErrorBase.erro_nec)]
        [JsonProperty(PropertyName = "password")]
        [MinLength(8, ErrorMessage = ErrorBase.erro_min)]
        public string password { get; set; }
        
        [JsonProperty(PropertyName = "doctorEntity")]
        public virtual ICollection<DoctorEntity> DoctorEntity { get; set; }

        // Hashear Senha
        public void HashearSenha()
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(this.Senha + ErrorBase.global_salt));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                this.Senha = builder.ToString();
            }
        }
    }
}

