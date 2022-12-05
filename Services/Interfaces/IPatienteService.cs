using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVida.Domain;
using Microsoft.Azure.Documents;
using ApiVida.Domain.Entities;

namespace ApiVida.Service.Interfaces
{
    public interface IPatienteService : IDisposable
    {
        //Cadastrar um cliente
        Task<Document> AddPatient(PatientEntity p);

        //Pegar um único cliente
        Task<PatientEntity> GetPatient(string cpf);

        Task<IEnumerable<PatientEntity>> ListPatients();
    }
}
