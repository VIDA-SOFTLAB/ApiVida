using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVida.Domain;
using Microsoft.Azure.Documents;
using ApiVida.Domain.Entities;

namespace ApiVida.Service.Interfaces
{
    public interface IMedicalCenterService : IDisposable
    {
        //Listagem de todos medicalinsurances
        //lista sem dependencia de nada!
        Task<IEnumerable<MedicalCenterEntity>> ListMedicalCentersFromMedicalInsurance(string idMedicalInsurance);


        //Listagem de todos MedicalCenter do sistema
        //lista sem dependencia de nada!
        Task<IEnumerable<MedicalCenterEntity>> ListMedicalCenters(string idAdm);


        //Cadastrar um MedicalCenter
        // nao tem dependencia de nada
        Task<MedicalCenterEntity> RegisterMedicalCenter(string idMedicalInsurance, MedicalCenterEntity medicalCenter);

        //Pegar um único medicalCenter
        Task<MedicalCenterEntity> GetMedicalCenter(string idMedicalCenter);

        //Atualizar um medicalCenter
        Task<MedicalCenterEntity> UpdateMedicalCenter(string idAdm, MedicalCenterEntity medicalCenter, string idMedicalInsurance);

        //Deletar um MedicalCenter
        Task DeleteMedicalCenter(string idAdm, string idMedicalCenter, string idMedicalInsurance);
    }
}
