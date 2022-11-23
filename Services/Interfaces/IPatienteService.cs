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
        Task<Document> CadastrarPatiente(Clientes cliente);

        //Pegar um único cliente
        Task<Clientes> PegarPatiente(string cpf);

        Task<IEnumerable<Clientes>> ListarPatientes();
    }
}
