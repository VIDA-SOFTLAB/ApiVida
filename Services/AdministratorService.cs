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
        public async Task<IEnumerable<Administrator>> ListarAdministrators()
        {
            List<Administrator> administrators = (List<Administrator>)await Repository<Administrator>.ListarAdm();
            
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
        public async Task<Administrator> CadastrarAdm(Administrator adm)
        {
            adm.PessoaJuridica = new List<PessoaJuridica>();
            adm.HashearSenha();
            await Repository<Administrator>.CadastrarItem(adm);
            adm = await Repository<Administrator>.PegarAdmPorEmail(adm.Email);

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
        public async Task<Administrator> PegarAdm(string id)
        {
            Administrator adm = await Repository<Administrator>.PegarAdm(id);

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
        public async Task<Document> AtualizarAdm(string id, Administrator adm)
        {
            Administrator admin = await Repository<Administrator>.PegarAdm(id);

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

            return await Repository<Administrator>.AtualizarAdm(id, adm);
        }


        //Deletar um adm
        public async Task DeletarAdm(string id)
        {
            try
            {
                await Repository<Administrator>.DeletarItem(id);
            }
            catch (ArgumentException e)
            {
                throw null;
            }

        }

    }
}
