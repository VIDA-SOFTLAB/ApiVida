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
    public class MedicalCenterService : IMedicalCenterService
    {
         public void Dispose()
        {
            //throw new NotImplementedException();
        }

        //Listagem de medicalcenters de medicalinsurance
        public async Task<IEnumerable<MedicalCenterEntity>> ListMedicalCentersFromMedicalInsurance(string idMedicalInsurance)
        {
            List<MedicalCenterEntity> medicalinsurances = (List<MedicalCenterEntity>)await Repository<MedicalCenterEntity>.ListMedicalCentersFromMedicalInsurance("MedicalCenter", idMedicalInsurance);

            if (medicalinsurances == null)
            {
                return null;
            }
            else
            {
                return medicalinsurances;
            }
        }


        //Listagem de medicalcenters
        public async Task<IEnumerable<MedicalCenterEntity>> ListMedicalCenters(string idAdm)
        {
            List<MedicalCenterEntity> medicalinsurances = (List<MedicalCenterEntity>)await Repository<MedicalCenterEntity>.ListMedicalCenters(idAdm, "MedicalCenter");

            if (medicalinsurances == null)
            {
                return null;
            }
            else
            {
                return medicalinsurances;
            }
        }


        //Cadastrar uma medicalcenter dentro de medicalinsurance
       public async Task<MedicalCenterEntity> RegisterMedicalCenter(string idMedicalInsurance, MedicalCenterEntity medicalCenter)
        {
///            MedicalInsuranceEntity mi = await Repository<MedicalInsuranceEntity>.GetMedicalCenter(idMedicalInsurance);

            MedicalCenterEntity mc = await Repository<MedicalCenterEntity>.GetMedicalCenterByName(medicalCenter.CenterName, "MedicalCenter");

            if (mc == null)
            {

                var retorno = await Repository<MedicalCenterEntity>.RegisterItem(medicalCenter);
                medicalCenter = await Repository<MedicalCenterEntity>.GetMedicalCenter(medicalCenter.Id, "MedicalCenter");

                medicalCenter.MedicalSpecialty = new List<MedicalSpecialityEntity>();
                medicalCenter.IdMedicalInsurance = new List<MedicalInsuranceEntity>();


                var retorno2 = await Repository<AdministratorEntityDTO>.RegisterMedicalCenter(medicalCenter, "MedicalCenter");

                await Repository<MedicalCenterEntity>.DeleteItem(medicalCenter.Id);

                if (medicalCenter == null || retorno == null || retorno2 == null)
                {
                    return null;
                }
                else
                {
                    return medicalCenter;
                }
            }
            else
            {
                Console.WriteLine("O nome inserido já está cadastrado! -- MEDICALCENTERS");
                return null;
            }
        }

        //Pegar uma única medicalinsurance
        public async Task<MedicalCenterEntity> GetMedicalCenter(string idMedicalCenter)
        {
            MedicalCenterEntity medicalinsurance = await Repository<MedicalCenterEntity>.GetMedicalCenter(idMedicalCenter, "MedicalCenter");

            if (medicalinsurance == null)
            {
                return null;
            }
            else
            {
                return medicalinsurance;
            }
        }


        //Atualizar uma medicalinsurance
        public async Task<MedicalCenterEntity> UpdateMedicalCenter(string idAdm, MedicalCenterEntity medicalCenter, string idMedicalInsurance)
        {
           // AdministratorEntityDTO adm = await Repository<AdministratorEntityDTO>.GetAdm(idAdm);

            MedicalInsuranceEntity mi = await Repository<MedicalInsuranceEntity>.GetMedicalInsurance(idMedicalInsurance, "MedicalInsurance");
            var retorno = await Repository<MedicalCenterEntity>.UpdateMedicalCenter(medicalCenter, mi, "MedicalCenter");

            if (mi == null || retorno == null)
            {
                return null;
            }
            else
            {
                return medicalCenter;
            }
        }


        //Deletar uma medicalinsurance
        public async Task DeleteMedicalCenter(string idAdm, string idMedicalCenter, string idMedicalInsurance)
        {
            MedicalCenterEntity medicalCenter = await Repository<MedicalCenterEntity>.GetMedicalCenter(idMedicalCenter, "MedicalCenter");
            AdministratorEntityDTO adm = await Repository<AdministratorEntityDTO>.GetAdm(idAdm);
            MedicalInsuranceEntity mi = await Repository<MedicalInsuranceEntity>.GetMedicalInsurance(idMedicalInsurance, "MedicalInsurance");


            if (medicalCenter == null || adm == null)
            {
                throw null;
            }
            else
            {
                await Repository<MedicalCenterEntity>.DeleteMedicalCenter(medicalCenter, mi);
            }
        }
    }
}
