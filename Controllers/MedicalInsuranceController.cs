using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVida.Repository;
using Microsoft.AspNetCore.Cors;
using ApiVida.Domain;
using ApiVida.Controllers;
using Microsoft.AspNetCore.Authorization;
using ApiVida.Service.Interfaces;
using ApiVida.Domain.Entities;

namespace ApiVida.Controllers
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
		[HttpGet("ListMedicalInsurancePlan/{idMedicalInsurance}")]
        public async Task<ActionResult<IEnumerable<MedicalInsurancePlanEntity>>> ListMedicalInsurancePlan(string idAdm)
        {
            // TODO: arrumar esat rota +  o que precisa para cadastrar
            List<MedicalInsurancePlanEntity> medicalinsurances = (List<MedicalInsurancePlanEntity>)await medicalinsuranceService.ListAllMedicalInsurancesPlans(idAdm);
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
        [HttpGet("ListMedicalInsurance")]
        //[HttpGet("all/{idAdm}/{idMedicalInsurance}")]
        public async Task<ActionResult<IEnumerable<MedicalInsuranceEntity>>> ListAllMedicalInsurances(string idAdm)
        {
            List<MedicalInsuranceEntity> medicalinsurances = (List<MedicalInsuranceEntity>)await medicalinsuranceService.ListAllMedicalInsurances();
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
        [HttpGet("specific/{id}")]
        public async Task<MedicalInsuranceEntity> GetMedicalInsurance(string idAdm, string id)
        {
            MedicalInsuranceEntity medicalinsurance = await medicalinsuranceService.ListMedicalInsuranceInfo(idAdm, id);
            return medicalinsurance;
        }

        //Cadastra um medicalinsurance
        //POST api/<controller>
        [HttpPost("{idAdm}")]
		public async Task<ActionResult<MedicalInsuranceEntity>> AddMedicalInsurance(string idAdm, [FromBody] MedicalInsuranceEntity medicalinsurance)
		{
            medicalinsurance = await medicalinsuranceService.RegisterMedicalInsurance(idAdm, medicalinsurance);

            if (medicalinsurance == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(medicalinsurance);
            }            
		}
        //Cadastra um medicalinsurance
        //POST api/<controller>
        [HttpPost("AddPlan/{idAdm}")]
        public async Task<ActionResult<MedicalInsurancePlanEntity>> AddMedicalInsurancePlan(string idAdm, [FromBody] List<MedicalInsurancePlanEntity> medicalinsurance)
        {
            var retorno = await medicalinsuranceService.RegisterMedicalInsurancePlan(idAdm, medicalinsurance);

            if (retorno == false)
            {
                return NotFound();
            }
            else
            {
                return Ok("Registrado com sucesso");
            }
        }

        //Atualiza registro de determinado medicalinsurance
        // PUT api/<controller>/5
        [HttpPut("{idAdm}/{id}")]
        public async Task<ActionResult<MedicalInsuranceEntity>> UpdateMedicalInsurance(string idAdm,string id, [FromBody]MedicalInsuranceEntity medicalinsuranceDTO)
        {
            MedicalInsuranceEntity serv = await medicalinsuranceService.GetMedicalInsurance(idAdm, id);

            if (serv != null)
            {
                if (medicalinsuranceDTO.EnterpriseName == null)
                {
                    medicalinsuranceDTO.EnterpriseName = serv.EnterpriseName;
                }

                //if (medicalinsuranceDTO.MedicalCenters == null)
                //{
                //    medicalinsuranceDTO.MedicalCenters = serv.MedicalCenters;
                //}

                if (medicalinsuranceDTO.MedicalPlans == null)
                {
                    medicalinsuranceDTO.MedicalPlans = serv.MedicalPlans;
                }

              
                medicalinsuranceDTO.EnterpriseId = id;
            }

            serv.EnterpriseName = medicalinsuranceDTO.EnterpriseName;
            //serv.MedicalCenters = medicalinsuranceDTO.MedicalCenters;
            serv.MedicalPlans = medicalinsuranceDTO.MedicalPlans;
            serv.EnterpriseId = id;

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