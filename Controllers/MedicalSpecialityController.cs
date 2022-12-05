using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiVida.Domain;
using Microsoft.AspNetCore.Cors;
using ApiVida.Repository;
using ApiVida.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ApiVida.Controllers;

namespace ApiVida.Controllers
{
	[Route("api/[controller]/")]
	[ApiController]
	[EnableCors("AllowAllHeaders")]
	[Authorize]
    public class MedicalSpecialityController : Controller
    {
		private readonly IMedicalSpecialityService medicalspecialityService;

	    public MedicalSpecialityController(IMedicalSpecialityService medicalspecialityService_)
		{
			this.medicalspecialityService = medicalspecialityService_;
		}
        
		//Lista todos os medicalspecialitys
		//GET api/medicalspeciality
		[HttpGet("{idAdm}/medicalspecialitys")]
        public async Task<ActionResult<IEnumerable<MedicalSpeciality>>> ListMedicalSpecialitys(string idAdm, string idPj)
        {
            List<MedicalSpeciality> medicalspecialitys = (List<MedicalSpeciality>)await medicalspecialityService.ListMedicalSpecialitys(idAdm, idPj);

            if (medicalspecialitys == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(medicalspecialitys);
            }
		}

        //Lista medicalspeciality específico
        // GET api/<controller>/5
        [HttpGet("specific/{idAdm}/{id}")]
        public async Task<MedicalSpeciality> PegarMedicalSpeciality(string idAdm, string id)
        {
            return await medicalspecialityService.PegarMedicalSpeciality(idAdm, id);
        }

		//Cadastra um medicalspeciality
		//POST api/<controller>
		[HttpPost("{idAdm}/medicalspecialitys")]
        public async Task<ActionResult<MedicalSpeciality>> CadastrarMedicalSpeciality(string idAdm, string idPj, [FromBody] MedicalSpeciality cat)
		{
            cat = await medicalspecialityService.CadastrarMedicalSpeciality(idAdm, idPj, cat);
            if (cat == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(cat);
            }
        }
        
		//Atualiza registro de determinado medicalspeciality
		// PUT api/<controller>/5
		[HttpPut("{idAdm}/{id}")]
        public async Task<ActionResult<MedicalSpeciality>> AtualizarMedicalSpeciality(string idAdm, string id, [FromBody]MedicalSpecialityDTO medicalspecialityDTO)
        {
            MedicalSpeciality esp = await medicalspecialityService.PegarMedicalSpeciality(idAdm, id);

            if (esp != null)
            {
                if (medicalspecialityDTO.Nome == null)
                {
                    medicalspecialityDTO.Nome = esp.Nome;
                }

                if (medicalspecialityDTO.Description == null)
                {
                    medicalspecialityDTO.Description = esp.Description;
                }

                if (medicalspecialityDTO.Doctor == null)
                {
                    medicalspecialityDTO.Doctor = esp.Doctor;
                }

                medicalspecialityDTO.Id = id;
            }

            esp.Nome = medicalspecialityDTO.Nome;
            esp.Description = medicalspecialityDTO.Description;
            esp.Doctor = medicalspecialityDTO.Doctor;
            esp.Id = id;

            var retorno = await medicalspecialityService.AtualizarMedicalSpeciality(idAdm, cat);

            if (retorno == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(retorno);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{idAdm}/{id}")]
        public async void DeletarMedicalSpeciality(string idAdm, string id)
        {
            await medicalspecialityService.DeletarMedicalSpeciality(idAdm, id);
        }
    }
}