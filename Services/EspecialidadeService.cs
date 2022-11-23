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
    public class EspecialidadeService : IEspecialidadeService
    {

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        //Listagem de especialidade
        public async Task<IEnumerable<Especialidade>> ListarEspecialidades(string idAdm)
        {
            List<Especialidade> especialidades = (List<Especialidade>)await Repository<Especialidade>.ListarEspecialidades(idAdm);

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
        public async Task<IEnumerable<Especialidade>> ListarTodosEspecialidades(string idAdm)
        {
            List<Especialidade> especialidades = (List<Especialidade>)await Repository<Especialidade>.ListarTodosEspecialidades(idAdm);

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
        public async Task<Especialidade> CadastrarEspecialidade(string idAdm, Especialidade especialidade)
        {
            Administrador adm = await Repository<Administrador>.PegarAdm(idAdm);

            Especialidade vali = await Repository<Especialidade>.PegarEspecialidadePorNome2(adm, especialidade.Nome);

            if (vali == null)
            {

                var retorno = await Repository<Especialidade>.CadastrarItem(especialidade);
              //  especialidade = await Repository<PessoaJuridica>.PegarEspecialidadePorNome(especialidade.Nome);

                especialidade.Profissional = new List<Profissional>();

                var retorno2 = await Repository<Administrador>.CadastrarEspecialidade(adm, especialidade);

                await Repository<Especialidade>.DeletarItem(especialidade.Id);

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
        public async Task<Especialidade> PegarEspecialidade(string idAdm, string id)
        {
            Especialidade especialidade = await Repository<Especialidade>.PegarEspecialidade(idAdm, id);

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
        public async Task<Especialidade> AtualizarEspecialidade(string idAdm, Especialidade especialidade)
        {
            Administrador adm = await Repository<Administrador>.PegarAdm(idAdm);
            var retorno = await Repository<Especialidade>.AtualizarEspecialidade(adm, especialidade);

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
        public async Task DeletarEspecialidade(string idAdm, string id)
        {
            Especialidade especialidade = await Repository<Especialidade>.PegarEspecialidade(idAdm, id);

            Administrador adm = await Repository<Administrador>.PegarAdm(idAdm);

            if (especialidade == null || adm == null)
            {
                throw null;
            }
            else
            {
                await Repository<Especialidade>.DeletarEspecialidade(especialidade, adm);
            }
        }
    }
}
