using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVida.Repository;
using ApiVida.Domain;
using ApiVida.Service.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using ApiVida.Controllers;
using ApiVida.Domain.Entities;

namespace ApiVida.Controllers
{

	[Route("api/[controller]/")]
    [ApiController]
    public class AdministratorController : Controller
	{
        private readonly IAdministratorService administratorService;

        public AdministratorController(IAdministratorService administratorService)
        {
            this.administratorService = administratorService;
        }

        //Lista todos os administratores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdmEntity>>> Get()
        {
            List<AdmEntity> administratores = (List<AdmEntity>)await administratorService.ListAdministrators();

            if (administratores == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(administratores);
            }
        }

        //Lista adm específico
        [HttpGet("{id}")]
        public async Task<AdmEntity> Get(string id)
        {
			return await administratorService.GetAdm(id);
        }

		[AllowAnonymous]
		[EnableCors("AllowAllHeaders")]
        //Cadastra um adm
        [HttpPost]
        //public async Task<ActionResult<Administrator>> Post([FromBody] Administrator administrator)
        public async Task<IActionResult> Post([FromBody] AdmEntity administrator)
        {
            AdmEntity admin = await administratorService.AddAdm(administrator);

            if (admin == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(admin);
            }
        }

        //Atualiza registro de determinado adm
        [HttpPut("{id}")]
        public async void Put(string id, [FromBody]AdmEntity adm)
        {
              await administratorService.UpdateAdm(id, adm);
        }

        // Deleta um adm especifico
        [HttpDelete("{id}")]
        public async void Delete(string id)
        {
            await administratorService.DeleteAdm(id);
        }
    }
}
