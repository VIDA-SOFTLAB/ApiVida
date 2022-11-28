using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athenas.Repository;
using Athenas.Domain;
using Athenas.Service.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace Athenas.Controllers
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
        public async Task<ActionResult<IEnumerable<Administrator>>> Get()
        {
            List<Administrator> administratores = (List<Administrator>)await administratorService.ListarAdministratores();

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
        public async Task<Administrator> Get(string id)
        {
			return await administratorService.GetAdm(id);
        }

		[AllowAnonymous]
		[EnableCors("AllowAllHeaders")]
        //Cadastra um adm
        [HttpPost]
        //public async Task<ActionResult<Administrator>> Post([FromBody] Administrator administrator)
        public async Task<IActionResult> Post([FromBody] Administrator administrator)
        {
            Administrator admin = await administratorService.AddAdm(administrator);

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
        public async void Put(string id, [FromBody]Administrator adm)
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
