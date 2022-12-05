using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVida.Domain;
using Microsoft.Azure.Documents;

namespace ApiVida.Service.Interfaces
{
    public interface IMedicalSpecialityService : IDisposable
    {
        //Listagem de todas especialidades
        Task<IEnumerable<MedicalSpeciality>> ListMedicalSpecialitys(string idAdm);

        //Cadastrar uma especialidade
        Task<MedicalSpeciality> CadastrarMedicalSpeciality(string idAdm, MedicalSpeciality esp);

        //Pegar uma única especialidade
        Task<MedicalSpeciality> PegarMedicalSpeciality(string idAdm, string id);

        //Atualizar uma especialidade
        Task<MedicalSpeciality> AtualizarMedicalSpeciality(string idAdm, MedicalSpeciality esp);

        //Deletar uma especialidade
        Task DeletarMedicalSpeciality(string idAdm, string id);
    }
}
