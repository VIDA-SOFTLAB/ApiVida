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
    public class MedicalCenterNewController : Controller
    {
        private readonly IMedicalCenterNewService medicalCenterService;

        public MedicalCenterNewController(IMedicalCenterNewService medicalCenterService_)
        {
            this.medicalCenterService = medicalCenterService_;
        }

        [HttpPost]
        public async void AddMedicalCenterNew([FromBody] MedicalCenterNewEntity exam, string idAdm)
        {
            AdmEntity adm = await Repository<AdmEntity>.GetAdm(idAdm, "Adm");

            await medicalCenterService.RegisterMedicalCenterNew(exam);
        }

        [HttpGet("ListarCentroMedico")]
        public async Task<ActionResult<IEnumerable<MedicalCenterNewEntity>>> ListMedicalCenterNews()
        {
            List<MedicalCenterNewEntity> exams = (List<MedicalCenterNewEntity>)await medicalCenterService.ListMedicalCenterNew();

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