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
            //throw new NotImplementedException();
        }

        //Listagem de medicalinsurance
       public async Task<MedicalInsuranceEntity> ListMedicalInsuranceInfo(string idAdm, string EnterpriseId)
        {
            List<MedicalInsuranceEntity> medicalinsurances = (List<MedicalInsuranceEntity>)await Repository<MedicalInsuranceEntity>.ListMedicalInsurances(idAdm, "MedicalInsurance");
            List<MedicalInsurancePlanEntity> medicalInsurancePlans = (List<MedicalInsurancePlanEntity>)await Repository<MedicalInsurancePlanEntity>.ListAllMedicalInsurancePlans("MedicalInsurancePlan");
            MedicalInsuranceEntity medicalInsurance = medicalinsurances.Where(m => m.EnterpriseId == EnterpriseId).FirstOrDefault();
            medicalInsurance.MedicalPlans = medicalInsurancePlans.Where(m => m.EnterpriseId == EnterpriseId).ToList();
            
            if (medicalInsurance == null)
            {
                return null;
            }
            else
            {
                return medicalInsurance;
            }
        }


        //Listagem de todos medicalinsurance do sistema
       public async Task<IEnumerable<MedicalInsuranceEntity>> ListAllMedicalInsurances()
        {
            List<MedicalInsuranceEntity> medicalinsurances = (List<MedicalInsuranceEntity>)await Repository<MedicalInsuranceEntity>.ListAllMedicalInsurances("MedicalInsurance");
            List<MedicalInsurancePlanEntity> medicalInsurancePlans = (List<MedicalInsurancePlanEntity>)await Repository<MedicalInsurancePlanEntity>.ListAllMedicalInsurancePlans("MedicalInsurancePlan");

            foreach (MedicalInsuranceEntity item in medicalinsurances)
            {
                item.MedicalPlans = medicalInsurancePlans.Where(x => x.EnterpriseId.Equals(item.EnterpriseId)).ToList();
            }
            

            if (medicalinsurances == null)
            {
                return null;
            }
            else
            {
                return medicalinsurances;
            }
        }
        public async Task<IEnumerable<MedicalInsurancePlanEntity>> ListAllMedicalInsurancesPlans(string EnterpriseId)
        {
            List<MedicalInsurancePlanEntity> medicalInsurancePlans = (List<MedicalInsurancePlanEntity>)await Repository<MedicalInsurancePlanEntity>.ListAllMedicalInsurancePlans("MedicalInsurancePlan");
            medicalInsurancePlans = medicalInsurancePlans.Where(p => p.EnterpriseId == EnterpriseId).ToList();

            if (medicalInsurancePlans == null)
            {
                return null;
            }
            else
            {
                return medicalInsurancePlans;
            }
        }

        //Cadastrar uma medicalinsurance
        public async Task<MedicalInsuranceEntity> RegisterMedicalInsurance(string idAdm, MedicalInsuranceEntity medicalinsurance)
        {
            AdmEntity adm = await Repository<AdmEntity>.GetAdm(idAdm, "Adm");

            //GetMedicalInsuranceByName = await Repository<MedicalInsuranceEntity>.GetMedicalInsuranceByName(medicalinsurance.EnterpriseName, "MedicalInsurance");
            string  vali = null;
            if (vali == null)
            {

                var retorno = await Repository<MedicalInsuranceEntity>.RegisterItem(medicalinsurance);
                medicalinsurance = await Repository<MedicalInsuranceEntity>.GetMedicalInsuranceByName(medicalinsurance.EnterpriseName, "MedicalInsurance");
                var retorno2 = await Repository<AdmEntity>.RegisterMedicalInsurance(medicalinsurance, "MedicalInsurance");
                List<MedicalInsurancePlanEntity> plans = new List<MedicalInsurancePlanEntity>(medicalinsurance.MedicalPlans);
                await Repository<MedicalInsuranceEntity>.DeleteItem(medicalinsurance.EnterpriseId);
                await Repository< MedicalInsurancePlanEntity >.RegisterMedicalInsurancePlan(plans, "MedicalInsurancePlan");
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

        public async Task<Boolean> RegisterMedicalInsurancePlan(string idAdm, List<MedicalInsurancePlanEntity> medicalinsuranceplan)
        {

               var retorno = await Repository<MedicalInsurancePlanEntity>.RegisterMedicalInsurancePlan(medicalinsuranceplan, "MedicalInsurancePlan");
            if (retorno == null)
            {
                return false;
            }
            else
            {
                return true;
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
            AdmEntity adm = await Repository<AdmEntity>.GetAdm(idAdm, "Adm");

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
            AdmEntity adm = await Repository<AdmEntity>.GetAdm(idAdm, "Adm");

            if (medicalinsurance == null || adm == null)
            {
                throw null;
            }
            else
            {
                await Repository<MedicalInsuranceEntity>.DeleteMedicalInsurance(medicalinsurance, "MedicalInsurance");
            }
        }
    }
}
