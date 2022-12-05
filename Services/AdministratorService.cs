﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiVida.Service.Interfaces;
using ApiVida.Domain;
using ApiVida.Repository;
using ApiVida.Service.Interfaces;
using ApiVida.Domain.Entities;
using Microsoft.Azure.Documents;

namespace ApiVida.Service
{
    public class AdministratorService : IAdministratorService
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }


        //Listagem de adm
        public async Task<IEnumerable<AdministratorEntityDTO>> ListAdministrators()
        {
            List<AdministratorEntityDTO> administrators = (List<AdministratorEntityDTO>)await Repository<AdministratorEntityDTO>.ListAdm();
            
            if (administrators == null)
            {
                throw new ArgumentException("O ID informado está incorreto ou ele não existe.", "adm");
            }
            else
            {
                return administrators;
            }
        }

        //Cadastrar um adm
        public async Task<AdministratorEntityDTO> AddAdm(AdministratorEntityDTO adm)
        {
            adm.HashearPassword();
            await Repository<AdministratorEntityDTO>.RegisterItem(adm);
            adm = await Repository<AdministratorEntityDTO>.GetAdmByEmail(adm.Email);

            if (adm == null)
            {
                throw new ArgumentException("Ocorreu um erro no cadastro. Por favor, verifique suas informações.", "adm");
            }
            else
            {
                return adm;
            }

            return adm;
        }

        //Pegar um único adm
        public async Task<AdministratorEntityDTO> GetAdm(string id)
        {
            AdministratorEntityDTO adm = await Repository<AdministratorEntityDTO>.GetAdm(id);

            if (adm == null)
            {
                throw new ArgumentException("O ID informado está incorreto ou ele não existe.", "adm");
            }
            else
            {
                return adm;
            }
        }


        //Atualizar um adm
        public async Task<Document> UpdateAdm(string id, AdministratorEntityDTO adm)
        {
            AdministratorEntityDTO admin = await Repository<AdministratorEntityDTO>.GetAdm(id);

            if (adm.Email == null)
            {
                adm.Email = admin.Email;
            }

            if (adm.Password == null)
            {
                adm.Password = admin.Password;
            }

            if (adm.Email == null)
            {
                adm.Email = admin.Email;
            }

            adm.Id = id;

            return await Repository<AdministratorEntityDTO>.UpdateAdm(id, adm);
        }


        //Deletar um adm
        public async Task DeleteAdm(string id)
        {
            try
            {
                await Repository<AdministratorEntityDTO>.DeletarItem(id);
            }
            catch (ArgumentException e)
            {
                throw null;
            }

        }

    }
}
