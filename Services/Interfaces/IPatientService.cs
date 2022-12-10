using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVida.Domain;
using Microsoft.Azure.Documents;
using ApiVida.Domain.Entities;

namespace ApiVida.Service.Interfaces
{
    public interface IPatientService : IDisposable
    {
        //Cadastrar um cliente
        Task<Document> RegisterPatient(PatientEntity p);

        //Pegar um único cliente
        Task<PatientEntity> GetPatient(string idPatient, string idAdm);
        Task<PatientEntity> GetPatientByCpf(string cpf, AdmEntity adm);

        Task<IEnumerable<PatientEntity>> ListPatients();

        //Atualizar um patient
		Task<Document> UpdatePatient(string id, PatientEntity p, string idAdm);

        //Deletar um patient
		Task DeletePatient(string id);
    }
}
