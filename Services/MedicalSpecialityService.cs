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
    public class MedicalSpecialityService //: IMedicalSpecialityService
    {

/*
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        //Listagem de especialidade
        public async Task<IEnumerable<MedicalSpecialityEntity>> ListMedicalSpecialitys(string idAdm)
        {
            List<MedicalSpecialityEntity> especialidades = (List<MedicalSpecialityEntity>)await Repository<MedicalSpecialityEntity>.ListMedical(idAdm);

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
        public async Task<IEnumerable<MedicalSpecialityEntity>> ListAllMedicalSpecialitys(string idAdm)
        {
            List<MedicalSpecialityEntity> especialidades = (List<MedicalSpecialityEntity>)await Repository<MedicalSpecialityEntity>.ListAllMedicalSpecialitys(idAdm);

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
        public async Task<MedicalSpecialityEntity> CadastrarMedicalSpeciality(string idAdm, MedicalSpecialityEntity especialidade)
        {
            Administrator adm = await Repository<Administrator>.GetAdm(idAdm);

            MedicalSpecialityEntity vali = await Repository<MedicalSpecialityEntity>.PegarMedicalSpecialityPorNome2(adm, especialidade.Nome);

            if (vali == null)
            {

                var retorno = await Repository<MedicalSpecialityEntity>.AddItem(especialidade);
              //  especialidade = await Repository<PessoaJuridica>.PegarMedicalSpecialityPorNome(especialidade.Nome);

                especialidade.Doctor = new List<Doctor>();

                var retorno2 = await Repository<Administrator>.CadastrarMedicalSpeciality(adm, especialidade);

                await Repository<MedicalSpecialityEntity>.DeleteItem(especialidade.Id);

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
        public async Task<MedicalSpecialityEntity> PegarMedicalSpeciality(string idAdm, string id)
        {
            MedicalSpecialityEntity especialidade = await Repository<MedicalSpecialityEntity>.PegarMedicalSpeciality(idAdm, id);

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
        public async Task<MedicalSpecialityEntity> AtualizarMedicalSpeciality(string idAdm, MedicalSpecialityEntity especialidade)
        {
            Administrator adm = await Repository<Administrator>.GetAdm(idAdm);
            var retorno = await Repository<MedicalSpecialityEntity>.AtualizarMedicalSpeciality(adm, especialidade);

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
            MedicalSpecialityEntity especialidade = await Repository<MedicalSpecialityEntity>.PegarMedicalSpeciality(idAdm, id);

            Administrator adm = await Repository<Administrator>.GetAdm(idAdm);

            if (especialidade == null || adm == null)
            {
                throw null;
            }
            else
            {
                await Repository<MedicalSpecialityEntity>.DeletarMedicalSpeciality(especialidade, adm);
            }
        }*/
    }
}
