using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athenas.Repository;
using Microsoft.AspNetCore.Cors;
using Athenas.Domain;
using Athenas.Controllers;
using Microsoft.AspNetCore.Authorization;
using Athenas.Service.Interfaces;

namespace Athenas.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
	[EnableCors("AllowAllHeaders")]
	[Authorize]
    public class MedicalInsuranceController : Controller
    {
		private readonly IMedicalInsuranceService medicalinsuranceService;

		public MedicalInsuranceController(IMedicalInsuranceService medicalinsuranceService_)
		{
			this.medicalinsuranceService = medicalinsuranceService_;
		}

		//Lista todos os medicalinsurances
		//GET api/medicalinsurance
		[HttpGet("{idAdm}/{idMedicalInsurance}")]
        public async Task<ActionResult<IEnumerable<MedicalInsurance>>> ListMedicalInsurances(string idAdm, string idCat)
        {
            List<MedicalInsurance> medicalinsurances = (List<MedicalInsurance>)await medicalinsuranceService.ListMedicalInsurances(idAdm, idCat);
            if (medicalinsurances == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(medicalinsurances);
            }
        }

        //Lista todos os medicalinsurances do sistema
        //GET api/medicalinsurance
        [HttpGet("all/{idAdm}/{idMedicalInsurance}")]
        public async Task<ActionResult<IEnumerable<MedicalInsurance>>> ListAllMedicalInsurances(string idAdm)
        {
            List<MedicalInsurance> medicalinsurances = (List<MedicalInsurance>)await medicalinsuranceService.ListAllMedicalInsurances(idAdm);
            if (medicalinsurances == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(medicalinsurances);
            }
        }

        //Lista medicalinsurance específico
        // GET api/<controller>/5
        [HttpGet("specific/{idMedicalInsurance}/{id}")]
        public async Task<MedicalInsurance> GetMedicalInsurance(string idAdm, string id)
        {
            MedicalInsurance medicalinsurance = await medicalinsuranceService.GetMedicalInsurance(idAdm, id);
            return medicalinsurance;
        }

        //Cadastra um medicalinsurance
        //POST api/<controller>
        [HttpPost("{idAdm}/{idMedicalInsurance}")]
		public async Task<ActionResult<MedicalInsurance>> AddMedicalInsurance(string idAdm, string idCat, [FromBody] MedicalInsurance medicalinsurance)
		{
            medicalinsurance = await medicalinsuranceService.AddMedicalInsurance(idAdm, idCat, medicalinsurance);

            if (medicalinsurance == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(medicalinsurance);
            }            
		}

        //Atualiza registro de determinado medicalinsurance
        // PUT api/<controller>/5
        [HttpPut("{idAdm}/{id}")]
        public async Task<ActionResult<MedicalInsurance>> UpdateMedicalInsurance(string idAdm,string id, [FromBody]MedicalInsuranceDTO medicalinsuranceDTO)
        {
            MedicalInsurance serv = await medicalinsuranceService.GetMedicalInsurance(idAdm, id);

            if (serv != null)
            {
                if (medicalinsuranceDTO.Nome == null)
                {
                    medicalinsuranceDTO.Nome = serv.Nome;
                }

                if (medicalinsuranceDTO.Description == null)
                {
                    medicalinsuranceDTO.Description = serv.Description;
                }

                if (medicalinsuranceDTO.Doctor == null)
                {
                    medicalinsuranceDTO.Doctor = serv.Doctor;
                }

                if (medicalinsuranceDTO.IdCategoria == null)
                {
                    medicalinsuranceDTO.IdCategoria = serv.IdCategoria;
                }
                medicalinsuranceDTO.Id = id;
            }

            serv.Nome = medicalinsuranceDTO.Nome;
            serv.Description = medicalinsuranceDTO.Description;
            serv.Doctor = medicalinsuranceDTO.Doctor;
            serv.IdCategoria = medicalinsuranceDTO.IdCategoria;
            serv.Id = id;

            var retorno = await medicalinsuranceService.UpdateMedicalInsurance(idAdm, serv);

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
        public async void DeleteMedicalInsurance(string idAdm, string id)
        {
            await medicalinsuranceService.DeleteMedicalInsurance(idAdm, id);
        }
    }
}