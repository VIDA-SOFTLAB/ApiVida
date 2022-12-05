using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVida.Domain;
using Microsoft.Azure.Documents;

namespace ApiVida.Service.Interfaces
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
