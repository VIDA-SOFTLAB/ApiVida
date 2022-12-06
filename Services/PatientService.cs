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
    public class PatientService : IPatientService
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        //Cadastrar um patient 
        public async Task<Document> RegisterPatient(PatientEntity patient)
        {
            return await Repository<PatientEntity>.RegisterPatient(patient);

        }

        //Pegar um único patient por cpf
        public async Task<PatientEntity> GetPatientByCpf(string cpf)
        {
            PatientEntity patients = await Repository<PatientEntity>.GetPatientByCpf(cpf, "Patient");

            if (patients == null)
            {
                return null;
            }
            else
            {
                return patients;
            }
        }

  //Pegar um único patient
        public async Task<PatientEntity> GetPatient(string id)
        {
            PatientEntity p = await Repository<PatientEntity>.GetPatient(id, "Patient");

            if (p == null)
            {
                throw new ArgumentException("O ID informado está incorreto ou ele não existe.", "PATIENT");
            }
            else
            {
                return p;
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

         //Atualizar um patient
        public async Task<Document> UpdatePatient(string idPatient, PatientEntity p)
        {
            PatientEntity patientDesatualizado = await Repository<PatientEntity>.GetPatient(idPatient, "Patient");

            if (p.Email == null)
            {
                p.Email = patientDesatualizado.Email;
            }

            if (p.Cpf == null)
            {
                p.Cpf = patientDesatualizado.Cpf;
            }

            if (p.PhoneNumber == null)
            {
                p.PhoneNumber = patientDesatualizado.PhoneNumber;
            }

            p.UserId = idPatient;

            return await Repository<PatientEntity>.UpdatePatient(idPatient, p, "Patient");
        }

          //Deletar um adm
        public async Task DeletePatient(string id)
        {
            try
            {
                await Repository<PatientEntity>.DeleteItem(id);
            }
            catch (ArgumentException e)
            {
                throw null;
            }

        }

    }
}
