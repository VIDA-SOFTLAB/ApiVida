using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVida.Domain;
using Microsoft.Azure.Documents;
namespace ApiVida.Service.Interfaces
{
    public interface IDoctorService : IDisposable
    {
        //Listagem de todos doctors
        Task<IEnumerable<Doctor>> ListDoctors(string idAdm, string idMedicalSpeciality);

        //Cadastrar um doctor
        Task<Doctor> AddDoctor(string idAdm, string idMedicalSpeciality, Doctor pro, Scheduling ag);

        //Pegar um único doctor
        Task<Doctor> GetDoctor(string idAdm, string id);

        //Atualizar um doctor
        Task<Doctor> UpdateDoctor(string idAdm, Doctor doctor, Scheduling ag);

        //Deletar um doctor
        Task DeleteDoctor(string idAdm, string id);

    }
}
