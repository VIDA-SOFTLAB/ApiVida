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
    public class MedicalInsuranceService : IMedicalInsuranceService
    {
         public void Dispose()
        {
            throw new NotImplementedException();
        }

        //Listagem de medicalinsurance
       public async Task<IEnumerable<MedicalInsuranceEntity>> ListMedicalInsurances(string idAdm)
        {
            List<MedicalInsuranceEntity> medicalinsurances = (List<MedicalInsuranceEntity>)await Repository<MedicalInsuranceEntity>.ListMedicalInsurances(idAdm, "MedicalInsurance");

            if (medicalinsurances == null)
            {
                return null;
            }
            else
            {
                return medicalinsurances;
            }
        }


        //Listagem de todos medicalinsurance do sistema
       public async Task<IEnumerable<MedicalInsuranceEntity>> ListAllMedicalInsurances()
        {
            List<MedicalInsuranceEntity> medicalinsurances = (List<MedicalInsuranceEntity>)await Repository<MedicalInsuranceEntity>.ListAllMedicalInsurances("MedicalInsurance");

            if (medicalinsurances == null)
            {
                return null;
            }
            else
            {
                return medicalinsurances;
            }
        }


        //Cadastrar uma medicalinsurance
        public async Task<MedicalInsuranceEntity> RegisterMedicalInsurance(string idAdm, MedicalInsuranceEntity medicalinsurance)
        {
            AdministratorEntityDTO adm = await Repository<AdministratorEntityDTO>.GetAdm(idAdm);

            MedicalInsuranceEntity vali = await Repository<MedicalInsuranceEntity>.GetMedicalInsuranceByName(medicalinsurance.EnterpriseName, "MedicalInsurance");

            if (vali == null)
            {

                var retorno = await Repository<MedicalInsuranceEntity>.RegisterItem(medicalinsurance);
                medicalinsurance = await Repository<MedicalInsuranceEntity>.GetMedicalInsuranceByName(medicalinsurance.EnterpriseName, "MedicalInsurance");

                medicalinsurance.MedicalCenters = new List<MedicalCenterEntity>();
                medicalinsurance.MedicalPlans = new List<MedicalInsurancePlanEntity>();

                var retorno2 = await Repository<AdministratorEntityDTO>.RegisterMedicalInsurance(medicalinsurance, "MedicalInsurance");

                await Repository<MedicalInsuranceEntity>.DeleteItem(medicalinsurance.EnterpriseId);

                if (adm == null || medicalinsurance == null || retorno == null || retorno2 == null)
                {
                    return null;
                }
                else
                {
                    return medicalinsurance;
                }
            }
            else
            {
                Console.WriteLine("O nome inserido já está cadastrado!");
                return null;
            }
        }

        //Pegar uma única medicalinsurance
        public async Task<MedicalInsuranceEntity> GetMedicalInsurance(string idAdm, string idMedicalInsurance)
        {
            MedicalInsuranceEntity medicalinsurance = await Repository<MedicalInsuranceEntity>.GetMedicalInsurance(idAdm, idMedicalInsurance);

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
        public async Task<MedicalInsuranceEntity> UpdateMedicalInsurance(string idAdm, MedicalInsuranceEntity medicalinsurance)
        {
            AdministratorEntityDTO adm = await Repository<AdministratorEntityDTO>.GetAdm(idAdm);

            var retorno = await Repository<MedicalInsuranceEntity>.UpdateMedicalInsurance( medicalinsurance, "MedicalInsurance");

            if (adm == null || retorno == null)
            {
                return null;
            }
            else
            {
                return medicalinsurance;
            }
        }


        //Deletar uma medicalinsurance
        public async Task DeleteMedicalInsurance(string idAdm, string idMedicalInsurance)
        {
            MedicalInsuranceEntity medicalinsurance = await Repository<MedicalInsuranceEntity>.GetMedicalInsurance(idAdm, idMedicalInsurance);
            AdministratorEntityDTO adm = await Repository<AdministratorEntityDTO>.GetAdm(idAdm);

            if (medicalinsurance == null || adm == null)
            {
                throw null;
            }
            else
            {
                await Repository<MedicalInsuranceEntity>.DeleteMedicalInsurance(medicalinsurance, medicalinsurance.MedicalCenters);
            }
        }
    }
}
