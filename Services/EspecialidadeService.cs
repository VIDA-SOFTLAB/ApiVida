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
    public class MedicalSpecialityService : IMedicalSpecialityService
    {

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        //Listagem de especialidade
        public async Task<IEnumerable<MedicalSpeciality>> ListMedicalSpecialitys(string idAdm)
        {
            List<MedicalSpeciality> especialidades = (List<MedicalSpeciality>)await Repository<MedicalSpeciality>.ListMedicalSpecialitys(idAdm);

            if (especialidades == null)
            {
                return null;
            }
            else
            {
                return especialidades;
            }
        }

        //Listagem de especialidade
        public async Task<IEnumerable<MedicalSpeciality>> ListAllMedicalSpecialitys(string idAdm)
        {
            List<MedicalSpeciality> especialidades = (List<MedicalSpeciality>)await Repository<MedicalSpeciality>.ListAllMedicalSpecialitys(idAdm);

            if (especialidades == null)
            {
                return null;
            }
            else
            {
                return especialidades;
            }
        }

        //Cadastrar um especialidade
        public async Task<MedicalSpeciality> CadastrarMedicalSpeciality(string idAdm, MedicalSpeciality especialidade)
        {
            Administrator adm = await Repository<Administrator>.GetAdm(idAdm);

            MedicalSpeciality vali = await Repository<MedicalSpeciality>.PegarMedicalSpecialityPorNome2(adm, especialidade.Nome);

            if (vali == null)
            {

                var retorno = await Repository<MedicalSpeciality>.AddItem(especialidade);
              //  especialidade = await Repository<PessoaJuridica>.PegarMedicalSpecialityPorNome(especialidade.Nome);

                especialidade.Doctor = new List<Doctor>();

                var retorno2 = await Repository<Administrator>.CadastrarMedicalSpeciality(adm, especialidade);

                await Repository<MedicalSpeciality>.DeleteItem(especialidade.Id);

                if (adm == null || especialidade == null || retorno == null || retorno2 == null)
                {
                    return null;
                }
                else
                {
                    return especialidade;
                }

            }
            else
            {
                Console.WriteLine("O nome inserido já está cadastrado!");
                return null;
            }
        }

        //Pegar um único especialidade
        public async Task<MedicalSpeciality> PegarMedicalSpeciality(string idAdm, string id)
        {
            MedicalSpeciality especialidade = await Repository<MedicalSpeciality>.PegarMedicalSpeciality(idAdm, id);

            if (especialidade == null)
            {
                return null;
            }
            else
            {
                return especialidade;
            }
        }

        //Atualizar um especialidade
        public async Task<MedicalSpeciality> AtualizarMedicalSpeciality(string idAdm, MedicalSpeciality especialidade)
        {
            Administrator adm = await Repository<Administrator>.GetAdm(idAdm);
            var retorno = await Repository<MedicalSpeciality>.AtualizarMedicalSpeciality(adm, especialidade);

            if (especialidade == null || adm == null || retorno == null)
            {
                return null;
            }
            else
            {
                return especialidade;
            }
        }

        //Deletar um especialidade
        public async Task DeletarMedicalSpeciality(string idAdm, string id)
        {
            MedicalSpeciality especialidade = await Repository<MedicalSpeciality>.PegarMedicalSpeciality(idAdm, id);

            Administrator adm = await Repository<Administrator>.GetAdm(idAdm);

            if (especialidade == null || adm == null)
            {
                throw null;
            }
            else
            {
                await Repository<MedicalSpeciality>.DeletarMedicalSpeciality(especialidade, adm);
            }
        }
    }
}
