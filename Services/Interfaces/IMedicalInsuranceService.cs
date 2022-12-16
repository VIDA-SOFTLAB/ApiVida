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
        //Retorna infos convenio especifico
        Task<MedicalInsuranceEntity> ListMedicalInsuranceInfo(string idAdm, string EnterpriseId);

        //Listagem de convenios
        Task<IEnumerable<MedicalInsuranceEntity>> ListAllMedicalInsurances();
        
        //Listagem de planos de saúde
        Task<IEnumerable<MedicalInsurancePlanEntity>> ListAllMedicalInsurancesPlans(string EnterpriseId);

        //Cadastrar um medicalinsurance
        Task<MedicalInsuranceEntity> RegisterMedicalInsurance(string idAdm, MedicalInsuranceEntity medicalinsurance);
        
        Task<Boolean> RegisterMedicalInsurancePlan(string idAdm, List<MedicalInsurancePlanEntity> medicalinsurance);

        //Pegar um único serviço
        Task<MedicalInsuranceEntity> GetMedicalInsurance(string idAdm, string idMedicalInsurance);

        //Atualizar um medicalinsurance
        Task<MedicalInsuranceEntity> UpdateMedicalInsurance(string idAdm, MedicalInsuranceEntity medicalinsurance);

        //Deletar um medicalinsurance
        Task DeleteMedicalInsurance(string idAdm, string idMedicalInsurance);
    }
}
