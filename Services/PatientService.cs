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
    public class PatientService : IPatienteService
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        //Cadastrar um patient
        public async Task<Document> CadastrarPatient(PatientEntity patients)
        {
            return await Repository<PatientEntity>.RegisterPatient(patients);

        }

        //Pegar um único patient
        public async Task<PatientEntity> PegarPatient(string cpf)
        {
            PatientEntity patients = await Repository<PatientEntity>.GetPatient(cpf);

            if (patients == null)
            {
                return null;
            }
            else
            {
                return patients;
            }
        }

        public async Task<IEnumerable<PatientEntity>> ListPatients()
        {
            List<PatientEntity> patients = (List<PatientEntity>)await Repository<PatientEntity>.ListPatients();

            if (patients == null)
            {
                throw new ArgumentException("O ID informado está incorreto ou ele não existe.", "adm");
            }
            else
            {
                return patients;
            }
        }

        public Task<Document> AddPatient(PatientEntity p)
        {
            throw new NotImplementedException();
        }

        public Task<PatientEntity> GetPatient(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
