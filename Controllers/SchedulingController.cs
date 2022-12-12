using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVida.Repository;
using Microsoft.AspNetCore.Cors;
using ApiVida.Service.Interfaces;
using ApiVida.Domain. Entities;

namespace ApiVida.Controllers
{
    [Route("api/[controller]/")]
    [EnableCors("AllowAllHeaders")]
    [ApiController]
    public class SchedulingEntityController : Controller
    {
        private readonly ISchedulingService schedulingService;

        public SchedulingEntityController(ISchedulingService schedulingService_)
        {
            this.schedulingService = schedulingService_;
        }

        //Lista todos os schedulings
        //GET api/scheduling
        [HttpGet("{idAdm}/{idDoctor}")]
        public async Task<ActionResult<IEnumerable<SchedulingEntity>>> ListSchedulingEntitys(string idAdm, string idDoctor)
        {
            List<SchedulingEntity> schedulings = (List<SchedulingEntity>)await schedulingService.ListSchedulings(idAdm, idDoctor);
            if (schedulings == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(schedulings);
            }
        }

        //Cadastra um scheduling
        //POST api/<controller>
        [HttpPost("{idAdm}/{idDoctor}")]
        public async Task<ActionResult<SchedulingEntity>> AddSchedulingEntity(string idAdm, string idDoctor, [FromBody] SchedulingEntity agen)
        {
            agen = await schedulingService.AddScheduling(idAdm, idDoctor, agen);
            if (agen == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(agen);
            }
        }

        //Lista scheduling específico
        // GET api/<controller>/5
        [HttpGet("specific/{idAdm}/{id}")]
        public async Task<SchedulingEntity> GetScheduling(string idAdm, string id)
        {
            SchedulingEntity scheduling = await schedulingService.GetScheduling(idAdm, id);
            return scheduling;
        }

        //Atualiza registro de determinado profissional
        // PUT api/<controller>/5
        [HttpPut("{idAdm}/{id}")]
        public async Task<ActionResult<SchedulingEntity>> UpdateScheduling(string idAdm, string id, [FromBody]SchedulingEntityDTO schedulingDTO)
        {
            SchedulingEntity scheduling = await schedulingService.GetScheduling(idAdm, id);

            if (scheduling != null)
            {
                if (schedulingDTO.DateComplet == null)
                {
                    schedulingDTO.DateComplet = scheduling.DateComplet;
                }

              

                if (schedulingDTO.IdDoctor == null)
                {
                    schedulingDTO.IdDoctor = scheduling.IdDoctor;
                }

                if (schedulingDTO.hour == null)
                {
                    schedulingDTO.hour = scheduling.hour;
                }

                 if (schedulingDTO.IdDoctor == null)
                {
                    schedulingDTO.IdDoctor = scheduling.IdDoctor;
                }
                schedulingDTO.Id = id;
            }

            scheduling.DateComplet = schedulingDTO.DateComplet;
            scheduling.hour = schedulingDTO.hour;
            scheduling.Patients = schedulingDTO.Patients;
            scheduling.IdDoctor = schedulingDTO.IdDoctor;
            scheduling.Id = id;

            var retorno = await schedulingService.UpdateScheduling(idAdm, scheduling);

            if (retorno == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(retorno);
            }
        }

        //// DELETE api/<controller>/5
        [HttpDelete("{idAdm}/{id}")]
        public async void DeleteScheduling(string idAdm, string id)
        {
            await schedulingService.DeleteScheduling(idAdm, id);
        }
    }
}