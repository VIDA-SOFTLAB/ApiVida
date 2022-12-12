using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ApiVida.Domain.Errors;

namespace ApiVida.Domain.Entities
{
    public enum BiologicalSexEnum
    {
        Feminino = 0,
        Masculino = 1,
        intersexo = 2
    }
}
