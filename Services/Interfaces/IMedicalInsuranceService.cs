using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVida.Domain;
using Microsoft.Azure.Documents;
using ApiVida.Domain.Entities;

namespace ApiVida.Service.Interfaces
{
    public interface IMedicalInsuranceService : IDisposable
    {
        //Listagem de todos medicalinsurances
        Task<IEnumerable<MedicalInsuranceEntity>> ListMedicalInsurances(string idAdm);


        //Listagem de todos medicalinsurances do sistema
        Task<IEnumerable<MedicalInsuranceEntity>> ListAllMedicalInsurances();


        //Cadastrar um medicalinsurance
        Task<MedicalInsuranceEntity> RegisterMedicalInsurance(string idAdm, MedicalInsuranceEntity medicalinsurance);

        //Pegar um único serviço
        Task<MedicalInsuranceEntity> GetMedicalInsurance(string idAdm, string idMedicalInsurance);

        //Atualizar um medicalinsurance
        Task<MedicalInsuranceEntity> UpdateMedicalInsurance(string idAdm, MedicalInsuranceEntity medicalinsurance);

        //Deletar um medicalinsurance
        Task DeleteMedicalInsurance(string idAdm, string idMedicalInsurance);
    }
}
