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
    public class MedicalInsuranceService //: IMedicalInsuranceService
    {
       /*  public void Dispose()
        {
            //throw new NotImplementedException();
        }

        //Listagem de medicalinsurance
/*        public async Task<IEnumerable<MedicalInsuranceEntity>> ListMedicalInsurances(string idAdm)
        {
            List<MedicalInsuranceEntity> medicalinsurances = (List<MedicalInsuranceEntity>)await Repository<MedicalInsuranceEntity>.ListMedicalInsurances(idAdm);

            if (medicalinsurances == null)
            {
                return null;
            }
            else
            {
                return medicalinsurances;
            }
        }
*/

        //Cadastrar uma medicalinsurance
/*        public async Task<MedicalInsuranceEntity> AddMedicalInsurance(string idAdm, MedicalInsuranceEntity medicalinsurance)
        {
            AdministratorEntityDTO adm = await Repository<AdministratorEntityDTO>.GetAdm(idAdm);

            MedicalInsuranceEntity vali = await Repository<MedicalInsuranceEntity>.GetMedicalInsurancePorNome2(adm, medicalinsurance.Nome);

            if (vali == null)
            {

                var retorno = await Repository<MedicalInsuranceEntity>.AddItem(medicalinsurance);
                medicalinsurance = await Repository<MedicalInsuranceEntity>.GetMedicalInsurancePorNome(medicalinsurance.Nome);

                medicalinsurance.Servico = new List<Servico>();

                var retorno2 = await Repository<AdministratorEntityDTO>.AddMedicalInsurance(adm, medicalinsurance);

                await Repository<MedicalInsuranceEntity>.DeleteItem(medicalinsurance.Id);

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
        public async Task<MedicalInsuranceEntity> GetMedicalInsurance(string idAdm, string id)
        {
            MedicalInsuranceEntity medicalinsurance = await Repository<MedicalInsuranceEntity>.GetMedicalInsurance(idAdm, id);

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
            var retorno = await Repository<MedicalInsuranceEntity>.UpdateMedicalInsurance(adm, medicalinsurance);

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
        public async Task DeleteMedicalInsurance(string idAdm, string id)
        {
            MedicalInsuranceEntity medicalinsurance = await Repository<MedicalInsuranceEntity>.GetMedicalInsurance(idAdm, id);
            AdministratorEntityDTO adm = await Repository<AdministratorEntityDTO>.GetAdm(idAdm);

            if (medicalinsurance == null || adm == null)
            {
                throw null;
            }
            else
            {
                await Repository<PessoaJuridica>.DeleteMedicalInsurance(medicalinsurance, adm);
            }
        }*/
    }
}
