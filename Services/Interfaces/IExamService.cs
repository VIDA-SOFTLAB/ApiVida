using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVida.Domain;
using Microsoft.Azure.Documents;
using ApiVida.Domain.Entities;

namespace ApiVida.Service.Interfaces
{
    public interface IExamService : IDisposable
    {
        //Cadastrar um cliente
        Task<Document> RegisterExam(ExamEntity p);

        Task<IEnumerable<ExamEntity>> ListExams();
        
        Task<IEnumerable<ExamEntity>> ListExamsByCPF(string cpf);
    }
}
