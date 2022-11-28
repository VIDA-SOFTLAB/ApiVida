using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athenas.Repository;
using Microsoft.AspNetCore.Cors;
using Athenas.Domain;
using Athenas.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Athenas.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
	[EnableCors("AllowAllHeaders")]
    [Authorize]
    public class DoctorController : Controller
    {
        private readonly IDoctorService doctorService;
        
        public DoctorController(IDoctorService doctorService_)
        {
            this.doctorService = doctorService_;
        }
        
        //Lista todos os profissionais
        //GET api/doctor
        [HttpGet("{idAdm}/{idMedicalSpeciality}")]
        public async Task<ActionResult<IEnumerable<Doctor>>> ListDoctors(string idAdm, string idMedicalSpeciality)
        {
            List<Doctor> profissionais = (List<Doctor>)await doctorService.ListDoctors(idAdm, idMedicalSpeciality);
            if (profissionais == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(profissionais);
            }
        }

        //Lista doctor específico
        // GET api/<controller>/5
        [HttpGet("specific/{idAdm}/{id}")]
        public async Task<Doctor> GetDoctor(string idAdm, string id)
        {
            Doctor doctor = await doctorService.GetDoctor(idAdm, id);
            return doctor;
        }

        //Cadastra Doctor
        //POST api/doctor
        [HttpPost("{idAdm}/{idMedicalSpeciality}")]
        public async Task<ActionResult<Doctor>> AddDoctor(string idAdm, string idMedicalSpeciality, [FromBody] Doctor pro)
        {
            pro = await doctorService.AddDoctor(idAdm, idMedicalSpeciality, pro);
            if (pro == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pro);
            }
        }

        //Atualiza registro de determinado Doctors
        // PUT api/doctor/2
        [HttpPut("{idAdm}/{id}")]
        public async Task<ActionResult<Doctor>>  UpdateDoctor(string idAdm, string id, [FromBody]DoctorDTO doctorDTO)
        {
            Doctor doctor = await doctorService.GetDoctor(idAdm, id);

            if (doctor != null)
            {
                if (doctorDTO.NomeCompleto == null)
                {
                    doctorDTO.NomeCompleto = doctor.NomeCompleto;
                }

                if (doctorDTO.Email == null)
                {
                    doctorDTO.Email = doctor.Email;
                }

                if (doctorDTO.Scheduling == null)
                {
                    doctorDTO.Scheduling = doctor.Scheduling;
                }

                if (doctorDTO.IdMedicalSpeciality == null)
                {
                    doctorDTO.IdMedicalSpeciality = doctor.IdMedicalSpeciality;
                }

                if (doctorDTO.Pin == null)
                {
                    doctorDTO.Pin = doctorDTO.Pin;
                }
                doctorDTO.Id = id;
            }

            doctor.NomeCompleto = doctorDTO.NomeCompleto;
            doctor.Email = doctorDTO.Email;
            doctor.Pin = doctorDTO.Pin;
            doctor.Scheduling = doctorDTO.Scheduling;
            doctor.IdMedicalSpeciality = doctorDTO.IdMedicalSpeciality;
            doctor.Id = id;

            var retorno = await doctorService.UpdateDoctor(idAdm, doctor);

            if (retorno == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(retorno);
            }
        }

        // DELETE api/doctor/5
        [HttpDelete("{idAdm}/{id}")]
        public async void Delete(string idAdm, string id)
        {
            await doctorService.DeleteDoctor(idAdm, id);
        }
    }
}