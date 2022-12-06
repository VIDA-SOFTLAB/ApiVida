using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVida.Domain;
using Microsoft.Azure.Documents;
using ApiVida.Domain.Entities;

namespace ApiVida.Service.Interfaces
{
    public interface IAdministratorService : IDisposable
    {

		//Listagem de todos os adm
        Task<IEnumerable<AdmEntity>> ListAdministrators();

		//Cadastrar um adm
		Task<AdmEntity> AddAdm(AdmEntity adm);

		//Pegar um único adm por seu id
		Task<AdministratorEntityDTO> GetAdm(string id);

		//Atualizar um adm
		Task<Document> UpdateAdm(string id, AdministratorEntityDTO adm);

		//Deletar um adm
		Task DeleteAdm(string id);
		

	}
}
