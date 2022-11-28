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
    public class MedicalInsuranceService : IMedicalInsuranceService
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        //Listagem de medicalinsurance
        public async Task<IEnumerable<MedicalInsurance>> ListMedicalInsurances(string idAdm)
        {
            List<MedicalInsurance> medicalinsurances = (List<MedicalInsurance>)await Repository<MedicalInsurance>.ListMedicalInsurances(idAdm);

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
        public async Task<MedicalInsurance> AddMedicalInsurance(string idAdm, MedicalInsurance medicalinsurance)
        {
            Administrator adm = await Repository<Administrator>.GetAdm(idAdm);
            medicalinsurance.IdPessoaJuridica = idPj;

            MedicalInsurance vali = await Repository<MedicalInsurance>.GetMedicalInsurancePorNome2(adm, medicalinsurance.Nome);

            if (vali == null)
            {

                var retorno = await Repository<MedicalInsurance>.AddItem(medicalinsurance);
                medicalinsurance = await Repository<MedicalInsurance>.GetMedicalInsurancePorNome(medicalinsurance.Nome);

                medicalinsurance.Servico = new List<Servico>();

                var retorno2 = await Repository<Administrator>.AddMedicalInsurance(adm, medicalinsurance);

                await Repository<MedicalInsurance>.DeleteItem(medicalinsurance.Id);

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
        public async Task<MedicalInsurance> GetMedicalInsurance(string idAdm, string id)
        {
            MedicalInsurance medicalinsurance = await Repository<MedicalInsurance>.GetMedicalInsurance(idAdm, id);

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
        public async Task<MedicalInsurance> UpdateMedicalInsurance(string idAdm, MedicalInsurance medicalinsurance)
        {
            Administrator adm = await Repository<Administrator>.GetAdm(idAdm);
            var retorno = await Repository<MedicalInsurance>.UpdateMedicalInsurance(adm, medicalinsurance);

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
            MedicalInsurance medicalinsurance = await Repository<MedicalInsurance>.GetMedicalInsurance(idAdm, id);
            Administrator adm = await Repository<Administrator>.GetAdm(idAdm);

            if (medicalinsurance == null || adm == null)
            {
                throw null;
            }
            else
            {
                await Repository<PessoaJuridica>.DeleteMedicalInsurance(medicalinsurance, adm);
            }
        }
    }
}
