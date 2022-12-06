using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiVida.JwtDomains;
using ApiVida.Domain;
using ApiVida.Repository;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Security.Cryptography;
using ApiVida.Domain.Entities;

namespace ApiVida.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AdmEntity loginModel)
        {
            //List<Administrator> administradores = (List<Administrator>)await Repository<Administrator>.ListAdm();

            //Administrator admin = administradores.FirstOrDefault(x => x.Email == loginModel.Email);

            AdmEntity admin = await Repository<AdmEntity>.GetAdmByEmail(loginModel.Email);

            if (admin == null)
                return Unauthorized();

            loginModel.HashearPassword();

            if (admin.Password != loginModel.Password)
            {
                admin = null;
            } 
            
            if (admin == null)
                return Unauthorized();
            else
            {
                var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create("a-password-very-big-to-be-good"))
                                .AddIssuer(admin.Email)
                                .AddAudience(admin.Email)
                                .AddNameId(admin.Id)
                                .AddClaim("administratorNow", admin.Id)
                                .Build();
                

                JObject o = new JObject
                {
                    { "token", token }
                };

                Console.WriteLine(o);
                return Ok(o);
            }
        }
    }
}