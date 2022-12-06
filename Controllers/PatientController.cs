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

namespace ApiVida.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
	[EnableCors("AllowAllHeaders")]
	[Authorize]
    public class PatientController : Controller
    {

        private readonly IPatientService patientService;

        public PatientController(IPatientService patientService_)
        {
            this.patientService = patientService_;
        }

        [HttpPost]
        public async void AddPatient([FromBody] PatientEntity patient)
        {
            await patientService.RegisterPatient(patient);
        }

        //Lista agendamento específico
        // GET api/<controller>/5
        [HttpGet("{cpf}")]
        public async Task<PatientEntity> PegarPatient(string cpf)
        {
            PatientEntity patient = await patientService.GetPatient(cpf);
            return patient;
        }

        //Lista todos os patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientEntity>>> ListPatients()
        {
            List<PatientEntity> patients = (List<PatientEntity>)await patientService.ListPatients();

            if (patients == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(patients);
            }
        }

    }
}