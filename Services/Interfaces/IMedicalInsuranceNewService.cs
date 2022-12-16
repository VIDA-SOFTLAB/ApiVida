using ApiVida.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Documents;
using System.Text;
using System.Threading.Tasks;

namespace ApiVida.Services.Interfaces
{
    public interface IMedicalInsuranceNewService : IDisposable
    {
        Task<Document> RegisterMedicalInsuranceNew(MedicalInsuranceNewEntity p);

        Task<IEnumerable<MedicalInsuranceNewEntity>> ListMedicalInsuranceNew();
    }
}
