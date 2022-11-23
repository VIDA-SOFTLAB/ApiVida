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
        [HttpGet("{idAdm}/{idEspecialidade}")]
        public async Task<ActionResult<IEnumerable<Doctor>>> ListarProfissionais(string idAdm, string idEspecialidade)
        {
            List<Doctor> profissionais = (List<Doctor>)await doctorService.ListarProfissionais(idAdm, idEspecialidade);
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
        public async Task<Doctor> PegarDoctor(string idAdm, string id)
        {
            Doctor doctor = await doctorService.PegarDoctor(idAdm, id);
            return doctor;
        }

        //Cadastra Doctor
        //POST api/doctor
        [HttpPost("{idAdm}/{idEspecialidade}")]
        public async Task<ActionResult<Doctor>> CadastrarDoctor(string idAdm, string idEspecialidade, [FromBody] Doctor pro)
        {
            pro = await doctorService.CadastrarDoctor(idAdm, idEspecialidade, pro);
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
        public async Task<ActionResult<Doctor>>  AtualizarDoctor(string idAdm, string id, [FromBody]DoctorDTO doctorDTO)
        {
            Doctor doctor = await doctorService.PegarDoctor(idAdm, id);

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

                if (doctorDTO.Agendamento == null)
                {
                    doctorDTO.Agendamento = doctor.Agendamento;
                }

                if (doctorDTO.IdEspecialidade == null)
                {
                    doctorDTO.IdEspecialidade = doctor.IdEspecialidade;
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
            doctor.Agendamento = doctorDTO.Agendamento;
            doctor.IdEspecialidade = doctorDTO.IdEspecialidade;
            doctor.Id = id;

            var retorno = await doctorService.AtualizarDoctor(idAdm, doctor);

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
            await doctorService.DeletarDoctor(idAdm, id);
        }
    }
}