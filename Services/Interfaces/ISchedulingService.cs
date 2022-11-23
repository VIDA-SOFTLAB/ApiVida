using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athenas.Domain;
using Microsoft.Azure.Documents;

namespace Athenas.Service.Interfaces
{
    public interface ISchedulingService : IDisposable
    {
        //Listagem de todos schedulings
        Task<IEnumerable<Scheduling>> ListarSchedulings(string idAdm, string idPro);

        //Cadastrar um scheduling
        Task<Scheduling> CadastrarScheduling(string idAdm, string idPro, Scheduling scheduling);

        //Pegar um único scheduling
        Task<Scheduling> PegarScheduling(string idAdm, string id);

        //Atualizar um scheduling
        Task<Scheduling> AtualizarScheduling(string idAdm, Scheduling scheduling);

        //Deletar um scheduling
        Task DeletarScheduling(string idAdm, string id);
    }
}
