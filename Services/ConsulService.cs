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
    public class ConsultService : IConsultService
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public async Task<Document> RegisterConsult(ConsultEntity consult)
        {
            return await Repository<ConsultEntity>.RegisterConsult(consult, "Consult");

        }

        public async Task<IEnumerable<ConsultEntity>> ListConsults()
        {
            List<ConsultEntity> consults = (List<ConsultEntity>)await Repository<ConsultEntity>.ListConsults("Consult");

            Console.WriteLine(consults);
            
            if (consults == null)
            {
                throw new ArgumentException("O ID informado está incorreto ou ele não existe.", "adm");
            }
            else
            {
                return consults;
            }
        }
        public async Task<IEnumerable<ConsultEntity>> ListConsultsByCPF(string cpf)
        {
            List<ConsultEntity> consults = (List<ConsultEntity>)await Repository<ConsultEntity>.ListConsults("Consult");
            List<ConsultEntity> filteredConsults = consults.FindAll(x => x.PatientCpf == cpf);

            if (filteredConsults == null)
            {
                return null;
            }
            else
            {
                return filteredConsults;
            }
        }
    }
}