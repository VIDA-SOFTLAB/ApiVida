using ApiVida.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Documents;
using System.Text;
using System.Threading.Tasks;

namespace ApiVida.Services.Interfaces
{
    public interface IMedicalCenterNewService : IDisposable
    {
        Task<Document> RegisterMedicalCenterNew(MedicalCenterNewEntity p);

        Task<IEnumerable<MedicalCenterNewEntity>> ListMedicalCenterNew();
    }
}
