using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVida.Repository;
using Microsoft.AspNetCore.Cors;
using ApiVida.Domain;
using ApiVida.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ApiVida.Domain.Entities;

namespace ApiVida.Controllers
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
        
        //Lista todos os doctors
        //GET api/doctor
        [HttpGet("{idAdm}/{idMedicalSpeciality}")]
        public async Task<ActionResult<IEnumerable<DoctorEntity>>> ListDoctors(string idAdm, string idMedicalSpeciality)
        {
            List<DoctorEntity> doctors = (List<DoctorEntity>)await doctorService.ListDoctors(idAdm, idMedicalSpeciality);
            if (doctors == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(doctors);
            }
        }

        //Lista doctor específico
        // GET api/<controller>/5
        [HttpGet("specific/{idAdm}/{id}")]
        public async Task<DoctorEntity> GetDoctor(string idAdm, string id)
        {
            DoctorEntity doctor = await doctorService.GetDoctor(idAdm, id);
            return doctor;
        }

        //Cadastra Doctor
        //POST api/doctor
        [HttpPost("{idAdm}/{idMedicalSpeciality}")]
        public async Task<ActionResult<DoctorEntity>> AddDoctor(string idAdm, string idMedicalSpeciality, [FromBody] DoctorEntity pro)
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
        public async Task<ActionResult<DoctorEntity>>  UpdateDoctor(string idAdm, string id, [FromBody]DoctorEntity doctorDTO)
        {
            DoctorEntity doctor = await doctorService.GetDoctor(idAdm, id);

            if (doctor != null)
            {
                if (doctorDTO.UserName == null)
                {
                    doctorDTO.UserName = doctor.UserName;
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

                if (doctorDTO.Adress == null)
                {
                    doctorDTO.Adress = doctorDTO.Adress;
                }
                doctorDTO.UserId = id;
            }

            doctor.UserName = doctorDTO.UserName;
            doctor.Email = doctorDTO.Email;
            doctor.IdMedicalSpeciality = doctorDTO.IdMedicalSpeciality;
            doctor.Scheduling = doctorDTO.Scheduling;
            doctorDTO.Adress = doctorDTO.Adress;
            doctor.UserId = id;

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