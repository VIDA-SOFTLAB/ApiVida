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
    public class ExamController : Controller
    {
        private readonly IExamService examService;

        public ExamController(IExamService examService_)
        {
            this.examService = examService_;
        }

        [HttpPost]
        public async void AddExam([FromBody] ExamEntity exam, string idAdm)
        {
            AdmEntity adm = await Repository<AdmEntity>.GetAdm(idAdm, "Adm");

            await examService.RegisterExam(exam);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamEntity>>> ListExams()
        {
            List<ExamEntity> exams = (List<ExamEntity>)await examService.ListExams();

            if (exams == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(exams);
            }
        }

        [HttpGet("cpf/{cpf}")]
        public async  Task<ActionResult<ExamEntity>> ListExamsByCPF(string cpf, string idAdm)
        {
            List<ExamEntity> exams = (List<ExamEntity>)await examService.ListExamsByCPF(cpf);

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