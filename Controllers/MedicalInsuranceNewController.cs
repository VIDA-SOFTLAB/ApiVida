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
using ApiVida.Services.Interfaces;

namespace ApiVida.Controllers
{
    [Route("api/{idAdm}/[controller]/")]
    [ApiController]
	[EnableCors("AllowAllHeaders")]
	[Authorize]
    public class MedicalInsuranceNewController : Controller
    {
        private readonly IMedicalInsuranceNewService medicalInsuranceService;

        public MedicalInsuranceNewController(IMedicalInsuranceNewService medicalInsuranceService_)
        {
            this.medicalInsuranceService = medicalInsuranceService_;
        }

        [HttpPost]
        public async void AddMedicalInsuranceNew([FromBody] MedicalInsuranceNewEntity exam, string idAdm)
        {
            AdmEntity adm = await Repository<AdmEntity>.GetAdm(idAdm, "Adm");

            await medicalInsuranceService.RegisterMedicalInsuranceNew(exam);
        }

        [HttpGet("ListarConvenio")]
        public async Task<ActionResult<IEnumerable<MedicalInsuranceNewEntity>>> ListMedicalInsuranceNews()
        {
            List<MedicalInsuranceNewEntity> exams = (List<MedicalInsuranceNewEntity>)await medicalInsuranceService.ListMedicalInsuranceNew();

            if (exams == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(exams);
            }
        }


    }
}