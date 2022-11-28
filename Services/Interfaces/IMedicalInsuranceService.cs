using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athenas.Domain;
using Microsoft.Azure.Documents;

namespace Athenas.Service.Interfaces
{
    public interface IMedicalInsuranceService : IDisposable
    {
        //Listagem de todos medicalinsurances
        Task<IEnumerable<MedicalInsurance>> ListMedicalInsurances(string idAdm);

        //Cadastrar um medicalinsurance
        Task<MedicalInsurance> AddMedicalInsurance(string idAdm, MedicalInsurance medicalinsurance);

        //Pegar um único serviço
        Task<MedicalInsurance> GetMedicalInsurance(string idAdm, string id);

        //Atualizar um medicalinsurance
        Task<MedicalInsurance> UpdateMedicalInsurance(string idAdm, MedicalInsurance medicalinsurance);

        //Deletar um medicalinsurance
        Task DeleteMedicalInsurance(string idAdm, string id);
    }
}
