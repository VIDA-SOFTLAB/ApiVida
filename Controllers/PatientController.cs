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
        public async void CadastrarPatient([FromBody] Patients patient)
        {
            await patientService.CadastrarPatient(patient);
        }

        //Lista agendamento específico
        // GET api/<controller>/5
        [HttpGet("{cpf}")]
        public async Task<Patients> PegarPatient(string cpf)
        {
            Patients patient = await patientService.PegarPatient(cpf);
            return patient;
        }

        //Lista todos os patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patients>>> ListPatients()
        {
            List<Patients> patients = (List<Patients>)await patientService.ListPatients();

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