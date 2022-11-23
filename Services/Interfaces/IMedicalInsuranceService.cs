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
        Task<IEnumerable<MedicalInsurance>> ListarMedicalInsurances(string idAdm);

        //Cadastrar um medicalinsurance
        Task<MedicalInsurance> CadastrarMedicalInsurance(string idAdm, MedicalInsurance medicalinsurance);

        //Pegar um único serviço
        Task<MedicalInsurance> PegarMedicalInsurance(string idAdm, string id);

        //Atualizar um medicalinsurance
        Task<MedicalInsurance> AtualizarMedicalInsurance(string idAdm, MedicalInsurance medicalinsurance);

        //Deletar um medicalinsurance
        Task DeletarMedicalInsurance(string idAdm, string id);
    }
}
