
using ApiVida.Domain.Entities;
using ApiVida.Repository;
using ApiVida.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Azure.Documents;
using System.Text;

namespace ApiVida.Services
{
    public class MedicalCenterNewService : IMedicalCenterNewService
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public async Task<Document> RegisterMedicalCenterNew(MedicalCenterNewEntity mi)
        {
            return await Repository<MedicalCenterNewEntity>.RegisterMedicalCenterNew(mi, "MINew");

        }

        public async Task<IEnumerable<MedicalCenterNewEntity>> ListMedicalCenterNew()
        {
            List<MedicalCenterNewEntity> mis = (List<MedicalCenterNewEntity>)await Repository<MedicalCenterNewEntity>.ListMedicalCenterNew("MINew");

            if (mis == null)
            {
                throw new ArgumentException("O ID informado está incorreto ou ele não existe.", "adm");
            }
            else
            {
                return mis;
            }
        }
    }
}