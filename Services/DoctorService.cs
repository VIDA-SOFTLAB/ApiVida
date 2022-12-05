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
    public class DoctorService : IDoctorService
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        //Listagem de doctor
        public async Task<IEnumerable<DoctorEntity>> ListDoctors(string idAdm, string idSer)
        {
            List<DoctorEntity> doctors = (List<DoctorEntity>)await Repository<DoctorEntity>.ListDoctorEntity(idAdm, idSer);
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
        public async Task<DoctorEntity> AddDoctor(string idAdm, string idMedicalSpeciality, DoctorEntity pro)
        {

            AdministratorEntityDTO adm = await Repository<AdministratorEntityDTO>.GetAdm(idAdm);
            pro.IdMedicalSpeciality = idMedicalSpeciality;

            DoctorEntity vali = await Repository<DoctorEntity>.GetDoctorEntityByEmail2(adm, pro.Email);

            if (vali == null)
            {
                var retorno = await Repository<DoctorEntity>.RegisterItem(pro);

                pro = await Repository<DoctorEntity>.GetDoctorEntityByEmail(pro.Email);

                pro.Scheduling = new List<SchedulingEntity>();

                var retorno2 = await Repository<AdministratorEntityDTO>.RegisterDoctorEntity(adm, pro);

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
        public async Task<DoctorEntity> GetDoctor(string idAdm, string id)
        {
            DoctorEntity doctor = await Repository<DoctorEntity>.GetDoctorEntity(idAdm, id);

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
        public async Task<DoctorEntity> UpdateDoctor(string idAdm, DoctorEntity doctor)
        {
            AdministratorEntityDTO adm = await Repository<AdministratorEntityDTO>.GetAdm(idAdm);
            var retorno = await Repository<DoctorEntity>.UpdateDoctorEntity(adm, doctor);

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
            DoctorEntity doctor = await Repository<DoctorEntity>.GetDoctorEntity(idAdm, id);
            AdministratorEntityDTO adm = await Repository<AdministratorEntityDTO>.GetAdm(idAdm);

            if (doctor == null || adm == null)
            {
                throw null;
            }
            else
            {
                await Repository<DoctorEntity>.DeleteDoctorEntity(doctor, adm);
            }
        }
    }
}
