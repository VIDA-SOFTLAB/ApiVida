using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ApiVida.Domain.Errors;

namespace ApiVida.Domain.Entities
{
    public class AdressEntityDTO
    {
        [Key]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [JsonProperty(PropertyName = "zipCode")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [MinLength(4, ErrorMessage = ErrorBase.erro_min)]
        [MaxLength(31, ErrorMessage = ErrorBase.erro_max)]
        [JsonProperty(PropertyName = "district")]
        public string District { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [MinLength(4, ErrorMessage = ErrorBase.erro_min)]
        [MaxLength(128, ErrorMessage = ErrorBase.erro_max)]
        [JsonProperty(PropertyName = "locality")]
        public string Locality { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [MinLength(2, ErrorMessage = ErrorBase.erro_min)]
        [MaxLength(2, ErrorMessage = ErrorBase.erro_max)]
        [JsonProperty(PropertyName = "fu")]
        public string fu { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [MinLength(3, ErrorMessage = ErrorBase.erro_min)]
        [MaxLength(31, ErrorMessage = ErrorBase.erro_max)]
        [JsonProperty(PropertyName = "publicPlace")]
        public string PublicPlace { get; set; }

        [Required(ErrorMessage = ErrorBase.erro_camponec)]
        [MinLength(1, ErrorMessage = ErrorBase.erro_min)]
        [MaxLength(7, ErrorMessage = ErrorBase.erro_max)]
        [JsonProperty(PropertyName = "number")]
        public string Number { get; set; }

        [MinLength(3, ErrorMessage = ErrorBase.erro_min)]
        [MaxLength(127, ErrorMessage = ErrorBase.erro_max)]
        [JsonProperty(PropertyName = "adressComplement")]
        public string AdressComplement { get; set; }
    }
}
