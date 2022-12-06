using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVida.Domain;
using Microsoft.Azure.Documents;
using ApiVida.Domain.Entities;

namespace ApiVida.Service.Interfaces
{
    public interface IMedicalSpecialityService : IDisposable
    {
        //Listagem de todas especialidades
        Task<IEnumerable<MedicalSpecialityEntity>> ListMedicalSpecialitys(string idAdm);

        //Cadastrar uma especialidade
        Task<MedicalSpecialityEntity> AddMedicalSpeciality(string idAdm, MedicalSpecialityEntity esp);

        //Pegar uma única especialidade
        Task<MedicalSpecialityEntity> GetMedicalSpeciality(string idAdm, string id);

        //Atualizar uma especialidade
        Task<MedicalSpecialityEntity> UpdateMedicalSpeciality(string idAdm, MedicalSpecialityEntity esp);

        //Deletar uma especialidade
        Task DeleteMedicalSpeciality(string idAdm, string id);
    }
}
