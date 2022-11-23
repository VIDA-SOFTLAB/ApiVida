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
    public class SchedulingService : ISchedulingService
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }


        //Listagem de scheduling
        public async Task<IEnumerable<Scheduling>> ListarSchedulings(string idAdm, string idPro)
        {
            List<Scheduling> schedulings = (List<Scheduling>)await Repository<Scheduling>.ListarSchedulings(idAdm, idPro);

            if (schedulings == null)
            {
                return null;
            }
            else
            {
                return schedulings;
            }
        }


        //Cadastrar um scheduling
        public async Task<Scheduling> CadastrarScheduling(string idAdm, string idProf, Scheduling sch)
        {
            Administrator adm = await Repository<Administrator>.PegarAdm(idAdm);
            sch.IdProfissional = idProf;

            Scheduling vali = await Repository<Scheduling>.PegarSchedulingPeloHorario2(adm, sch.Horario);

            if (vali == null)
            {

                var retorno = await Repository<Scheduling>.CadastrarItem(sch);

                sch = await Repository<Scheduling>.PegarSchedulingPeloHorario(sch.Horario);

                sch.Cliente = new List<Clientes>();

                var retorno2 = await Repository<Administrator>.CadastrarScheduling(adm, sch);

                await Repository<Servico>.DeletarItem(sch.Id);

                if (adm == null || sch == null || retorno == null || retorno2 == null)
                {
                    return null;
                }
                else
                {
                    return sch;
                }
            }
            else
            {
                Console.WriteLine("O horário inserido já está cadastrado!");
                return null;
            }
        }

        //Pegar um único scheduling
        public async Task<Scheduling> PegarScheduling(string idAdm, string id)
        {
            Scheduling scheduling = await Repository<Scheduling>.PegarScheduling(idAdm, id);

            if (scheduling == null)
            {
                return null;
            }
            else
            {
                return scheduling;
            }
        }


        //Atualizar um scheduling
        public async Task<Scheduling> AtualizarScheduling(string idAdm, Scheduling scheduling)
        {
            Administrator adm = await Repository<Administrator>.PegarAdm(idAdm);
            var retorno = await Repository<Scheduling>.AtualizarScheduling(adm, scheduling);

            if (scheduling == null || adm == null || retorno == null)
            {
                return null;
            }
            else
            {
                return scheduling;
            }
        }


        //Deletar um scheduling
        public async Task DeletarScheduling(string idAdm, string id)
        {
            Scheduling scheduling = await Repository<Scheduling>.PegarScheduling(idAdm, id);

            Administrator adm = await Repository<Administrator>.PegarAdm(idAdm);

            if (scheduling == null || adm == null)
            {
                throw null;
            }
            else
            {
                await Repository<Scheduling>.DeletarScheduling(scheduling, adm);
            }
        }
    }
}
