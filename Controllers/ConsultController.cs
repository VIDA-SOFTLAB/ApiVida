using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiVida.Domain;
using ApiVida.Repository;
using Microsoft.AspNetCore.Cors;
using ApiVida.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ApiVida.Domain.Entities;
using ApiVida.Service;
using ApiVida.Domain.Entities;

namespace ApiVida.Controllers
{
    [Route("api/{idAdm}/[controller]/")]
    [ApiController]
	[EnableCors("AllowAllHeaders")]
	[Authorize]
    public class ConsultController : Controller
    {
        private readonly IConsultService consultService;

        public ConsultController(IConsultService consultService_)
        {
            this.consultService = consultService_;
        }

        [HttpPost]
        public async void AddConsult([FromBody] ConsultEntity consult, string idAdm)
        {
            AdmEntity adm = await Repository<AdmEntity>.GetAdm(idAdm, "Adm");

            await consultService.RegisterConsult(consult);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultEntity>>> ListConsults()
        {
            List<ConsultEntity> consults = (List<ConsultEntity>)await consultService.ListConsults();

            if (consults == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(consults);
            }
        }

        [HttpGet("cpf/{cpf}")]
        public async  Task<ActionResult<ConsultEntity>> ListConsultsByCPF(string cpf, string idAdm)
        {
            List<ConsultEntity> consults = (List<ConsultEntity>)await consultService.ListConsultsByCPF(cpf);

            if (consults == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(consults);
            }
        }
    }
}