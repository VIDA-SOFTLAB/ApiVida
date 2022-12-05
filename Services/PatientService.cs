using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiVida.Service.Interfaces;
using Microsoft.Azure.Documents;
using ApiVida.Domain;
using ApiVida.Repository;

namespace ApiVida.Service
{
    public class PatientService : IPatientService
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        //Cadastrar um patient
        public async Task<Document> CadastrarPatient(Patients patients)
        {
            return await Repository<Patients>.CadastrarPatient(patients);

        }

        //Pegar um único patient
        public async Task<Patients> PegarPatient(string cpf)
        {
            Patients patients = await Repository<Patients>.PegarPatient(cpf);

            if (patients == null)
            {
                return null;
            }
            else
            {
                return patients;
            }
        }

        public async Task<IEnumerable<Patients>> ListPatients()
        {
            List<Patients> patients = (List<Patients>)await Repository<Patients>.ListPatients();

            if (patients == null)
            {
                throw new ArgumentException("O ID informado está incorreto ou ele não existe.", "adm");
            }
            else
            {
                return patients;
            }
        }
    }
}
