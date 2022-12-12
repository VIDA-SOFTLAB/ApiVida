using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiVida.Service.Interfaces;
using Microsoft.Azure.Documents;
using ApiVida.Domain.Entities;
using ApiVida.Repository;
using ApiVida.Service.Interfaces;

namespace ApiVida.Service
{
    public class ExamService : IExamService
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public async Task<Document> RegisterExam(ExamEntity exam)
        {
            return await Repository<ExamEntity>.RegisterExam(exam, "Exam");

        }

        public async Task<IEnumerable<ExamEntity>> ListExams()
        {
            List<ExamEntity> exams = (List<ExamEntity>)await Repository<ExamEntity>.ListExams("Exam");

            if (exams == null)
            {
                throw new ArgumentException("O ID informado está incorreto ou ele não existe.", "adm");
            }
            else
            {
                return exams;
            }
        }
        public async Task<IEnumerable<ExamEntity>> ListExamsByCPF(string cpf)
        {
            List<ExamEntity> exams = (List<ExamEntity>)await Repository<ExamEntity>.ListExams("Exam");
            List<ExamEntity> filteredExams = exams.FindAll(x => x.PatientCpf == cpf);

            if (filteredExams == null)
            {
                return null;
            }
            else
            {
                return filteredExams;
            }
        }

    }
}