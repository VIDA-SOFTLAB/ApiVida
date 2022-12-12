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
using ApiVida.Domain.Entities;

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
        public async Task<ActionResult<IEnumerable<MedicalSpecialityEntity>>> ListMedicalSpecialitys(string idAdm, string idPj)
        {
            List<MedicalSpecialityEntity> medicalspecialitys = (List<MedicalSpecialityEntity>)await medicalspecialityService.ListMedicalSpecialitys(idAdm);

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
        public async Task<MedicalSpecialityEntity> PegarMedicalSpeciality(string idAdm, string id)
        {
            return await medicalspecialityService.GetMedicalSpeciality(idAdm, id);
        }

		//Cadastra um medicalspeciality
		//POST api/<controller>
		[HttpPost("{idAdm}/medicalspecialitys")]
        public async Task<ActionResult<MedicalSpecialityEntity>> CadastrarMedicalSpeciality(string idAdm, string idPj, [FromBody] MedicalSpecialityEntity ms)
		{
            ms = await medicalspecialityService.AddMedicalSpeciality(idAdm, ms);
            if (ms == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(ms);
            }
        }
        
		//Atualiza registro de determinado medicalspeciality
		// PUT api/<controller>/5
		[HttpPut("{idAdm}/{id}")]
        public async Task<ActionResult<MedicalSpecialityEntity>> AtualizarMedicalSpeciality(string idAdm, string id, [FromBody]MedicalSpecialityEntity medicalspecialityDTO)
        {
            MedicalSpecialityEntity esp = await medicalspecialityService.GetMedicalSpeciality(idAdm, id);

            if (esp != null)
            {
                if (medicalspecialityDTO.MedicalSpecialtyName == null)
                {
                    medicalspecialityDTO.MedicalSpecialtyName = esp.MedicalSpecialtyName;
                }

                if (medicalspecialityDTO.Description == null)
                {
                    medicalspecialityDTO.Description = esp.Description;
                }

                if (medicalspecialityDTO.doctorsEspeciality == null)
                {
                    medicalspecialityDTO.doctorsEspeciality = esp.doctorsEspeciality;
                }

                medicalspecialityDTO.MedicalSpecialtyId = id;
            }

            esp.MedicalSpecialtyName = medicalspecialityDTO.MedicalSpecialtyName;
            esp.Description = medicalspecialityDTO.Description;
            esp.doctorsEspeciality = medicalspecialityDTO.doctorsEspeciality;
            esp.MedicalSpecialtyId = id;

            var retorno = await medicalspecialityService.UpdateMedicalSpeciality(idAdm, medicalspecialityDTO);

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
            await medicalspecialityService.DeleteMedicalSpeciality(idAdm, id);
        }
    }
}