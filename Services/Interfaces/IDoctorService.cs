using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVida.Domain;
using Microsoft.Azure.Documents;
using ApiVida.Domain.Entities;

namespace ApiVida.Service.Interfaces
{
    public interface IDoctorService : IDisposable
    {
        //Listagem de todos doctors
        Task<IEnumerable<DoctorEntity>> ListDoctors(string idAdm, string idMedicalSpeciality);

        //Cadastrar um doctor
        Task<DoctorEntity> AddDoctor(string idAdm, string idMedicalSpeciality, DoctorEntity pro);

        //Pegar um único doctor
        Task<DoctorEntity> GetDoctor(string idAdm, string id);

        //Atualizar um doctor
        Task<DoctorEntity> UpdateDoctor(string idAdm, DoctorEntity doctor);

        //Deletar um doctor
        Task DeleteDoctor(string idAdm, string id);

    }
}
