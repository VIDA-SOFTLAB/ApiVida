using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athenas.Domain;
using Microsoft.Azure.Documents;

namespace Athenas.Service.Interfaces
{
    public interface IPatienteService : IDisposable
    {
        //Cadastrar um cliente
        Task<Document> AddPatient(Clientes cliente);

        //Pegar um único cliente
        Task<Clientes> GetPatient(string cpf);

        Task<IEnumerable<Clientes>> ListPatients();
    }
}
