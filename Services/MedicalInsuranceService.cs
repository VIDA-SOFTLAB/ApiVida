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
        public async Task<IEnumerable<MedicalInsurance>> ListarMedicalInsurances(string idAdm)
        {
            List<MedicalInsurance> medicalinsurances = (List<MedicalInsurance>)await Repository<MedicalInsurance>.ListarMedicalInsurances(idAdm);

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
        public async Task<MedicalInsurance> CadastrarMedicalInsurance(string idAdm, MedicalInsurance medicalinsurance)
        {
            Administrador adm = await Repository<Administrador>.PegarAdm(idAdm);
            medicalinsurance.IdPessoaJuridica = idPj;

            MedicalInsurance vali = await Repository<MedicalInsurance>.PegarMedicalInsurancePorNome2(adm, medicalinsurance.Nome);

            if (vali == null)
            {

                var retorno = await Repository<MedicalInsurance>.CadastrarItem(medicalinsurance);
                medicalinsurance = await Repository<MedicalInsurance>.PegarMedicalInsurancePorNome(medicalinsurance.Nome);

                medicalinsurance.Servico = new List<Servico>();

                var retorno2 = await Repository<Administrador>.CadastrarMedicalInsurance(adm, medicalinsurance);

                await Repository<MedicalInsurance>.DeletarItem(medicalinsurance.Id);

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
        public async Task<MedicalInsurance> PegarMedicalInsurance(string idAdm, string id)
        {
            MedicalInsurance medicalinsurance = await Repository<MedicalInsurance>.PegarMedicalInsurance(idAdm, id);

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
        public async Task<MedicalInsurance> AtualizarMedicalInsurance(string idAdm, MedicalInsurance medicalinsurance)
        {
            Administrador adm = await Repository<Administrador>.PegarAdm(idAdm);
            var retorno = await Repository<MedicalInsurance>.AtualizarMedicalInsurance(adm, medicalinsurance);

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
        public async Task DeletarMedicalInsurance(string idAdm, string id)
        {
            MedicalInsurance medicalinsurance = await Repository<MedicalInsurance>.PegarMedicalInsurance(idAdm, id);
            Administrador adm = await Repository<Administrador>.PegarAdm(idAdm);

            if (medicalinsurance == null || adm == null)
            {
                throw null;
            }
            else
            {
                await Repository<PessoaJuridica>.DeletarMedicalInsurance(medicalinsurance, adm);
            }
        }
    }
}
