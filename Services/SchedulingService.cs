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
        public async Task<IEnumerable<Scheduling>> ListSchedulings(string idAdm, string idPro)
        {
            List<Scheduling> schedulings = (List<Scheduling>)await Repository<Scheduling>.ListSchedulings(idAdm, idPro);

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
        public async Task<Scheduling> AddScheduling(string idAdm, string idProf, Scheduling sch)
        {
            Administrator adm = await Repository<Administrator>.GetAdm(idAdm);
            sch.IdDoctor = idProf;

            Scheduling vali = await Repository<Scheduling>.GetSchedulingPeloHorario2(adm, sch.Horario);

            if (vali == null)
            {

                var retorno = await Repository<Scheduling>.AddItem(sch);

                sch = await Repository<Scheduling>.GetSchedulingPeloHorario(sch.Horario);

                sch.Cliente = new List<Clientes>();

                var retorno2 = await Repository<Administrator>.AddScheduling(adm, sch);

                await Repository<Servico>.DeleteItem(sch.Id);

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
        public async Task<Scheduling> GetScheduling(string idAdm, string id)
        {
            Scheduling scheduling = await Repository<Scheduling>.GetScheduling(idAdm, id);

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
        public async Task<Scheduling> UpdateScheduling(string idAdm, Scheduling scheduling)
        {
            Administrator adm = await Repository<Administrator>.GetAdm(idAdm);
            var retorno = await Repository<Scheduling>.UpdateScheduling(adm, scheduling);

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
        public async Task DeleteScheduling(string idAdm, string id)
        {
            Scheduling scheduling = await Repository<Scheduling>.GetScheduling(idAdm, id);

            Administrator adm = await Repository<Administrator>.GetAdm(idAdm);

            if (scheduling == null || adm == null)
            {
                throw null;
            }
            else
            {
                await Repository<Scheduling>.DeleteScheduling(scheduling, adm);
            }
        }
    }
}
