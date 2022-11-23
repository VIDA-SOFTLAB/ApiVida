using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athenas.Domain;
using Microsoft.Azure.Documents;

namespace Athenas.Service.Interfaces
{
    public interface IAdministratorService : IDisposable
    {

		//Listagem de todos os adm
        Task<IEnumerable<Administrator>> ListarAdministrators();

		//Cadastrar um adm
		Task<Administrator> CadastrarAdm(Administrator adm);

		//Pegar um único adm por seu id
		Task<Administrator> PegarAdm(string id);

		//Atualizar um adm
		Task<Document> AtualizarAdm(string id, Administrator adm);

		//Deletar um adm
		Task DeletarAdm(string id);
		

	}
}
