using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVida.Domain;
using Microsoft.Azure.Documents;
using ApiVida.Domain.Entities;

namespace ApiVida.Service.Interfaces
{
    public interface ISchedulingService : IDisposable
    {
        //Listagem de todos schedulings
        Task<IEnumerable<SchedulingEntity>> ListSchedulings(string idAdm, string idPro);

        //Cadastrar um scheduling
        Task<SchedulingEntity> AddScheduling(string idAdm, string idPro, SchedulingEntity scheduling);

        //Pegar um único scheduling
        Task<SchedulingEntity> GetScheduling(string idAdm, string id);

        //Atualizar um scheduling
        Task<SchedulingEntity> UpdateScheduling(string idAdm, SchedulingEntity scheduling);

        //Deletar um scheduling
        Task DeleteScheduling(string idAdm, string id);
    }
}
