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
    public class MedicalCenterController : Controller
    {
		private readonly IMedicalCenterService medicalCenterService;

	    public MedicalCenterController(IMedicalCenterService medicalCenterService_)
		{
			this.medicalCenterService = medicalCenterService_;
		}
        
		//Lista todos os medicalcenters
		//GET api//idadm/idMedicalInsurance
		[HttpGet("{idAdm}/{idMedicalInsurance}")]
        public async Task<ActionResult<IEnumerable<MedicalCenterEntity>>> ListMedicalCenters(string idAdm, string idMedicalInsurance)
        {
            List<MedicalCenterEntity> medicalspecialitys = (List<MedicalCenterEntity>)await medicalCenterService.ListMedicalCentersFromMedicalInsurance(idMedicalInsurance);

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
        [HttpGet("specific/{idAdm}/{idMedicalInsurance}")]
        public async Task<MedicalCenterEntity> PegarMedicalCenter(string idAdm, string idMedicalInsurance)
        {
            return await medicalCenterService.GetMedicalCenter(idMedicalInsurance);
        }

		//Cadastra um medicalspeciality
		//POST api/<controller>
		[HttpPost("{idAdm}")]
        public async Task<ActionResult<MedicalCenterEntity>> RegisterMedicalCenter(string idMedicalInsurance, string idPj, [FromBody] MedicalCenterEntity ms)
		{
            ms = await medicalCenterService.RegisterMedicalCenter(idMedicalInsurance, ms);
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
		[HttpPut("{idAdm}/{idMedicalInsurance}/{idMedicalCenter}")]
        public async Task<ActionResult<MedicalCenterEntity>> AtualizarMedicalCenter(string idAdm, string idMedicalInsurance, string idMedicalCenter, [FromBody]MedicalCenterEntity medicalCenter)
        {
            MedicalCenterEntity medicalCenterNovo = await medicalCenterService.GetMedicalCenter(idMedicalCenter);

            if (medicalCenterNovo != null)
            {
                if (medicalCenter.CenterName == null)
                {
                    medicalCenter.CenterName = medicalCenterNovo.CenterName;
                }

                if (medicalCenter.CenterAdress == null)
                {
                    medicalCenter.CenterAdress = medicalCenterNovo.CenterAdress;
                }

                if (medicalCenter.MedicalSpecialty == null)
                {
                    medicalCenter.MedicalSpecialty = medicalCenterNovo.MedicalSpecialty;
                }

                if (medicalCenter.IdMedicalInsurance == null)
                {
                    medicalCenter.IdMedicalInsurance = medicalCenterNovo.IdMedicalInsurance;
                }

                medicalCenter.Id = idMedicalCenter;
            }

            medicalCenterNovo.CenterName = medicalCenter.CenterName;
            medicalCenterNovo.CenterAdress = medicalCenter.CenterAdress;
            medicalCenterNovo.MedicalSpecialty = medicalCenter.MedicalSpecialty;
            medicalCenterNovo.Id = idMedicalCenter;

            var retorno = await medicalCenterService.UpdateMedicalCenter(idAdm, medicalCenter, idMedicalInsurance);

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
        public async void DeletarMedicalCenter(string idAdm, string idMedicalCenter, string idMedicalInsurance)
        {
            await medicalCenterService.DeleteMedicalCenter(idAdm, idMedicalCenter, idMedicalInsurance);
        }
    }
}