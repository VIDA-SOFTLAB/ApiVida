using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athenas.Repository;
using Microsoft.AspNetCore.Cors;
using Athenas.Service.Interfaces;
using Athenas.Domain;

namespace Athenas.Controllers
{
    [Route("api/[controller]/")]
    [EnableCors("AllowAllHeaders")]
    [ApiController]
    public class SchedulingController : Controller
    {
        private readonly ISchedulingService schedulingService;

        public SchedulingController(ISchedulingService schedulingService_)
        {
            this.schedulingService = schedulingService_;
        }

        //Lista todos os schedulings
        //GET api/scheduling
        [HttpGet("{idAdm}/{idDoctor}")]
        public async Task<ActionResult<IEnumerable<Scheduling>>> ListSchedulings(string idAdm, string idDoctor)
        {
            List<Scheduling> schedulings = (List<Scheduling>)await schedulingService.ListSchedulings(idAdm, idDoctor);
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
        public async Task<ActionResult<Scheduling>> AddScheduling(string idAdm, string idDoctor, [FromBody] Scheduling agen)
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
        public async Task<Scheduling> GetScheduling(string idAdm, string id)
        {
            Scheduling scheduling = await schedulingService.GetScheduling(idAdm, id);
            return scheduling;
        }

        //Atualiza registro de determinado profissional
        // PUT api/<controller>/5
        [HttpPut("{idAdm}/{id}")]
        public async Task<ActionResult<Scheduling>> UpdateScheduling(string idAdm, string id, [FromBody]SchedulingDTO schedulingDTO)
        {
            Scheduling scheduling = await schedulingService.GetScheduling(idAdm, id);

            if (scheduling != null)
            {
                if (schedulingDTO.Dia == null)
                {
                    schedulingDTO.Dia = scheduling.Dia;
                }

                if (schedulingDTO.Horario == null)
                {
                    schedulingDTO.Horario = scheduling.Horario;
                }

                if (schedulingDTO.Cliente == null)
                {
                    schedulingDTO.Cliente = scheduling.Cliente;
                }

                if (schedulingDTO.IdDoctor == null)
                {
                    schedulingDTO.IdDoctor = scheduling.IdDoctor;
                }
                schedulingDTO.Id = id;
            }

            scheduling.Dia = schedulingDTO.Dia;
            scheduling.Horario = schedulingDTO.Horario;
            scheduling.Cliente = schedulingDTO.Cliente;
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