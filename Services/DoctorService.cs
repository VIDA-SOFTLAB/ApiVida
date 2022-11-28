using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Athenas.Service.Interfaces;
using Microsoft.Azure.Documents;
using Athenas.Domain;
using Athenas.Repository;

namespace Athenas.Service
{
    public class DoctorService : IDoctorService
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        //Listagem de doctor
        public async Task<IEnumerable<Doctor>> ListDoctors(string idAdm, string idSer)
        {
            List<Doctor> doctors = (List<Doctor>)await Repository<Doctor>.ListDoctors(idAdm, idSer);
            if (doctors == null)
            {
                return null;
            }
            else
            {
                return doctors;
            }
        }

        //Cadastrar um doctor
        public async Task<Doctor> AddDoctor(string idAdm, string IdMedicalSpeciality, Doctor pro)
        {

            Administrator adm = await Repository<Administrator>.GetAdm(idAdm);
            pro.IdServico = idMedicalSpeciality;

            Doctor vali = await Repository<Doctor>.GetDoctorPorEmail2(adm, pro.Email);

            if (vali == null)
            {
                var retorno = await Repository<Doctor>.AddItem(pro);

                pro = await Repository<Doctor>.GetDoctorPorEmail(pro.Email);

                pro.Scheduling = new List<Scheduling>();

                var retorno2 = await Repository<Administrator>.AddDoctor(adm, pro);

                await Repository<Servico>.DeleteItem(pro.Id);

                if (adm == null || pro == null || retorno == null || retorno2 == null)
                {
                    return null;
                }
                else
                {
                    return pro;
                }

            }
            else
            {
                Console.WriteLine("O email inserido já está cadastrado!");
                return null;
            }
        }

        //Pegar um único doctor
        public async Task<Doctor> GetDoctor(string idAdm, string id)
        {
            Doctor doctor = await Repository<Doctor>.GetDoctor(idAdm, id);

            if (doctor == null)
            {
                throw null;
            }
            else
            {
                return doctor;
            }
        }

        //Atualizar um doctor
        public async Task<Doctor> UpdateDoctor(string idAdm, Doctor doctor)
        {
            Administrator adm = await Repository<Administrator>.GetAdm(idAdm);
            var retorno = await Repository<Doctor>.UpdateDoctor(adm, doctor);

            if (doctor == null || adm == null || retorno == null)
            {
                return null;
            }
            else
            {
                return doctor;
            }
        }

        //Deletar um doctor
        public async Task DeleteDoctor(string idAdm, string id)
        {
            Doctor doctor = await Repository<Doctor>.GetDoctor(idAdm, id);
            Administrator adm = await Repository<Administrator>.GetAdm(idAdm);

            if (doctor == null || adm == null)
            {
                throw null;
            }
            else
            {
                await Repository<Doctor>.DeleteDoctor(doctor, adm);
            }
        }
    }
}
