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

       // GET api/<controller>/5
        [HttpGet("get/{cpf}")]
        public async  Task<ActionResult<PatientEntity>> GetPatientByCpf(string cpf)
        {
            try{
                Console.WriteLine("get by cpf");
                PatientEntity patient = await patientService.GetPatientByCpf(cpf);

                if(patient != null){
                    return Ok(patient);
                } else{
                    return NotFound();
                }

            }catch(Exception e){
                Console.WriteLine("erro: ", e.Message);
                return null;
            }
        }


        // GET api/<controller>/5
        [HttpGet("{idPatient}")]
        public async Task<PatientEntity> GetPatient(string idPatient)
        {

            Console.WriteLine("procura por id PATIENT");

            return await patientService.GetPatient(idPatient);
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

          //Atualiza registro de determinado adm
        [HttpPut("{idPatient}")]
        public async void Put(string idPatient, [FromBody]PatientEntity p)
        {
            Console.WriteLine("atualizando: ", idPatient);
              await patientService.UpdatePatient(idPatient, p);
        }

  // Deleta um patient especifico
        [HttpDelete("{idPatient}")]
        public async void Delete(string idPatient)
        {
            await patientService.DeletePatient(idPatient);
        }

    }
}