using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVida.Domain;
using Microsoft.Azure.Documents;
using ApiVida.Domain.Entities;

namespace ApiVida.Service.Interfaces
{
    public interface IConsultService : IDisposable
    {
        //Cadastrar um cliente
        Task<Document> RegisterConsult(ConsultEntity p);

        Task<IEnumerable<ConsultEntity>> ListConsults();
        
        Task<IEnumerable<ConsultEntity>> ListConsultsByCPF(string cpf);
    }
}
