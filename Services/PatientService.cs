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
            return await Repository<PatientEntity>.RegisterPatient(patient, "PatientUpdated2");

        }

        //Pegar um único patient por cpf
        public async Task<PatientEntity> GetPatientByCpf(string cpf, AdmEntity adm)
        {
            PatientEntity patients = await Repository<PatientEntity>.GetPatientByCpf(adm, cpf, "PatientUpdated2");

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
        public async Task<PatientEntity> GetPatient(string id, string idAdm)
        {
            AdmEntity adm = await Repository<AdmEntity>.GetAdm(idAdm, "Adm");

            PatientEntity p = await Repository<PatientEntity>.GetPatient(adm, id, "PatientUpdated2");

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
            List<PatientEntity> patients = (List<PatientEntity>)await Repository<PatientEntity>.ListPatients("Patient");

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
        public async Task<Document> UpdatePatient(string idPatient, PatientEntity p, string idAdm)
        {
            AdmEntity adm = await Repository<AdmEntity>.GetAdm(idAdm, "Adm");

            PatientEntity patientDesatualizado = await Repository<PatientEntity>.GetPatient(adm, idPatient, "PatientUpdated2");


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
