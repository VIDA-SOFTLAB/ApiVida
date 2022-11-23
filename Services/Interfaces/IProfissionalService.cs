using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athenas.Domain;
using Microsoft.Azure.Documents;
namespace Athenas.Service.Interfaces
{
    public interface IDoctorService : IDisposable
    {
        //Listagem de todos profissionais
        Task<IEnumerable<Doctor>> ListarProfissionais(string idAdm, string idEspecialidade);

        //Cadastrar um doctor
        Task<Doctor> CadastrarDoctor(string idAdm, string idEspecialidade, Doctor pro, Agendamento ag);

        //Pegar um único doctor
        Task<Doctor> PegarDoctor(string idAdm, string id);

        //Atualizar um doctor
        Task<Doctor> AtualizarDoctor(string idAdm, Doctor doctor, Agendamento ag);

        //Deletar um doctor
        Task DeletarDoctor(string idAdm, string id);

    }
}
