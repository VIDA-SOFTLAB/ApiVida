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
    [Route("api/{idAdm}/[controller]/")]
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
        public async void AddPatient([FromBody] PatientEntity patient, string idAdm)
        {
            AdmEntity adm = await Repository<AdmEntity>.GetAdm(idAdm, "Adm");

            await patientService.RegisterPatient(patient, adm);
        }

       // GET api/<controller>/5
        [HttpGet("cpf/{cpf}")]
        public async  Task<ActionResult<PatientEntity>> GetPatientByCpf(string cpf, string idAdm)
        {
            try{
                Console.WriteLine("get by cpf");

                AdmEntity adm = await Repository<AdmEntity>.GetAdm(idAdm, "Adm");
            List<PatientEntity> patients = (List<PatientEntity>)await Repository<PatientEntity>.ListPatients("PatientUpdated2");
            PatientEntity p = patients.FirstOrDefault(x => x.Cpf == cpf);

             //   PatientEntity patient = await patientService.GetPatientByCpf(cpf, adm);

                if(p != null){
                    return Ok(p);
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
        public async  Task<ActionResult<PatientEntity>> GetPatient(string idAdm, string idPatient)
        {

           try{
                Console.WriteLine("get by cpf");

                AdmEntity adm = await Repository<AdmEntity>.GetAdm(idAdm, "Adm");
            List<PatientEntity> patients = (List<PatientEntity>)await Repository<PatientEntity>.ListPatients("PatientUpdated2");
            PatientEntity p = patients.FirstOrDefault(x => x.UserId == idPatient);

             //   PatientEntity patient = await patientService.GetPatientByCpf(cpf, adm);

                if(p != null){
                    return Ok(p);
                } else{
                    return NotFound();
                }

            }catch(Exception e){
                Console.WriteLine("erro: ", e.Message);
                return null;
            }

//            return await patientService.GetPatient(idAdm, idPatient);
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
        public async void Put(string idAdm, string idPatient, [FromBody]PatientEntity p)
        {
            Console.WriteLine("atualizando: ", idPatient);
              await patientService.UpdatePatient(idPatient, p, idAdm);
        }

  // Deleta um patient especifico
        [HttpDelete("{idPatient}")]
        public async void Delete(string idPatient)
        {
            await patientService.DeletePatient(idPatient);
        }

    }
}