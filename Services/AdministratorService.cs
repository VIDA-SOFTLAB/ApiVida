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
    public class AdministratorService : IAdministratorService
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }


        //Listagem de adm
        public async Task<IEnumerable<Administrator>> ListAdministrators()
        {
            List<Administrator> administrators = (List<Administrator>)await Repository<Administrator>.ListAdm();
            
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
        public async Task<Administrator> AddAdm(Administrator adm)
        {
            adm.PessoaJuridica = new List<PessoaJuridica>();
            adm.HashearSenha();
            await Repository<Administrator>.AddItem(adm);
            adm = await Repository<Administrator>.GetAdmPorEmail(adm.Email);

            if (adm == null)
            {
                throw new ArgumentException("Ocorreu um erro no cadastro. Por favor, verifique suas informações.", "adm");
            }
            else
            {
                return adm;
            }
        }

        //Pegar um único adm
        public async Task<Administrator> GetAdm(string id)
        {
            Administrator adm = await Repository<Administrator>.GetAdm(id);

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
        public async Task<Document> UpdateAdm(string id, Administrator adm)
        {
            Administrator admin = await Repository<Administrator>.GetAdm(id);

            if (adm.NomeCompleto == null)
            {
                adm.NomeCompleto = admin.NomeCompleto;
            }

            if (adm.Senha == null)
            {
                adm.Senha = admin.Senha;
            }

            if (adm.Email == null)
            {
                adm.Email = admin.Email;
            }

            adm.Id = id;

            return await Repository<Administrator>.UpdateAdm(id, adm);
        }


        //Deletar um adm
        public async Task DeleteAdm(string id)
        {
            try
            {
                await Repository<Administrator>.DeleteItem(id);
            }
            catch (ArgumentException e)
            {
                throw null;
            }

        }

    }
}
