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
        public async Task<ActionResult<IEnumerable<MedicalInsurance>>> ListarMedicalInsurances(string idAdm, string idCat)
        {
            List<MedicalInsurance> medicalinsurances = (List<MedicalInsurance>)await medicalinsuranceService.ListarMedicalInsurances(idAdm, idCat);
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
        public async Task<ActionResult<IEnumerable<MedicalInsurance>>> ListarTodosMedicalInsurances(string idAdm)
        {
            List<MedicalInsurance> medicalinsurances = (List<MedicalInsurance>)await medicalinsuranceService.ListarTodosMedicalInsurances(idAdm);
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
        public async Task<MedicalInsurance> PegarMedicalInsurance(string idAdm, string id)
        {
            MedicalInsurance medicalinsurance = await medicalinsuranceService.PegarMedicalInsurance(idAdm, id);
            return medicalinsurance;
        }

        //Cadastra um medicalinsurance
        //POST api/<controller>
        [HttpPost("{idAdm}/{idMedicalInsurance}")]
		public async Task<ActionResult<MedicalInsurance>> CadastrarMedicalInsurance(string idAdm, string idCat, [FromBody] MedicalInsurance medicalinsurance)
		{
            medicalinsurance = await medicalinsuranceService.CadastrarMedicalInsurance(idAdm, idCat, medicalinsurance);

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
        public async Task<ActionResult<MedicalInsurance>> AtualizarMedicalInsurance(string idAdm,string id, [FromBody]MedicalInsuranceDTO medicalinsuranceDTO)
        {
            MedicalInsurance serv = await medicalinsuranceService.PegarMedicalInsurance(idAdm, id);

            if (serv != null)
            {
                if (medicalinsuranceDTO.Nome == null)
                {
                    medicalinsuranceDTO.Nome = serv.Nome;
                }

                if (medicalinsuranceDTO.Descricao == null)
                {
                    medicalinsuranceDTO.Descricao = serv.Descricao;
                }

                if (medicalinsuranceDTO.Profissional == null)
                {
                    medicalinsuranceDTO.Profissional = serv.Profissional;
                }

                if (medicalinsuranceDTO.IdCategoria == null)
                {
                    medicalinsuranceDTO.IdCategoria = serv.IdCategoria;
                }
                medicalinsuranceDTO.Id = id;
            }

            serv.Nome = medicalinsuranceDTO.Nome;
            serv.Descricao = medicalinsuranceDTO.Descricao;
            serv.Profissional = medicalinsuranceDTO.Profissional;
            serv.IdCategoria = medicalinsuranceDTO.IdCategoria;
            serv.Id = id;

            var retorno = await medicalinsuranceService.AtualizarMedicalInsurance(idAdm, serv);

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
        public async void DeletarMedicalInsurance(string idAdm, string id)
        {
            await medicalinsuranceService.DeletarMedicalInsurance(idAdm, id);
        }
    }
}