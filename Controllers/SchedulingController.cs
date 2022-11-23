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
        public async Task<ActionResult<IEnumerable<Scheduling>>> ListarSchedulings(string idAdm, string idDoctor)
        {
            List<Scheduling> schedulings = (List<Scheduling>)await schedulingService.ListarSchedulings(idAdm, idDoctor);
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
        public async Task<ActionResult<Scheduling>> CadastrarScheduling(string idAdm, string idDoctor, [FromBody] Scheduling agen)
        {
            agen = await schedulingService.CadastrarScheduling(idAdm, idDoctor, agen);
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
        public async Task<Scheduling> PegarScheduling(string idAdm, string id)
        {
            Scheduling scheduling = await schedulingService.PegarScheduling(idAdm, id);
            return scheduling;
        }

        //Atualiza registro de determinado profissional
        // PUT api/<controller>/5
        [HttpPut("{idAdm}/{id}")]
        public async Task<ActionResult<Scheduling>> AtualizarScheduling(string idAdm, string id, [FromBody]SchedulingDTO schedulingDTO)
        {
            Scheduling scheduling = await schedulingService.PegarScheduling(idAdm, id);

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

                if (schedulingDTO.IdProfissional == null)
                {
                    schedulingDTO.IdProfissional = scheduling.IdProfissional;
                }
                schedulingDTO.Id = id;
            }

            scheduling.Dia = schedulingDTO.Dia;
            scheduling.Horario = schedulingDTO.Horario;
            scheduling.Cliente = schedulingDTO.Cliente;
            scheduling.IdProfissional = schedulingDTO.IdProfissional;
            scheduling.Id = id;

            var retorno = await schedulingService.AtualizarScheduling(idAdm, scheduling);

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
        public async void DeletarScheduling(string idAdm, string id)
        {
            await schedulingService.DeletarScheduling(idAdm, id);
        }
    }
}