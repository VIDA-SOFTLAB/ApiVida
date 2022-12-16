
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
    public class MedicalInsuranceNewService : IMedicalInsuranceNewService
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public async Task<Document> RegisterMedicalInsuranceNew(MedicalInsuranceNewEntity mi)
        {
            return await Repository<MedicalInsuranceNewEntity>.RegisterMedicalInsuranceNew(mi, "MINew");

        }

        public async Task<IEnumerable<MedicalInsuranceNewEntity>> ListMedicalInsuranceNew()
        {
            List<MedicalInsuranceNewEntity> mis = (List<MedicalInsuranceNewEntity>)await Repository<MedicalInsuranceNewEntity>.ListMedicalInsuranceNew("MINew");

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