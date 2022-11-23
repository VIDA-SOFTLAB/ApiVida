using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athenas.Domain;
using Microsoft.Azure.Documents;

namespace Athenas.Service.Interfaces
{
    public interface IEspecialidadeService : IDisposable
    {
        //Listagem de todas especialidades
        Task<IEnumerable<Especialidade>> ListarEspecialidades(string idAdm);

        //Cadastrar uma especialidade
        Task<Especialidade> CadastrarEspecialidade(string idAdm, Especialidade esp);

        //Pegar uma única especialidade
        Task<Especialidade> PegarEspecialidade(string idAdm, string id);

        //Atualizar uma especialidade
        Task<Especialidade> AtualizarEspecialidade(string idAdm, Especialidade esp);

        //Deletar uma especialidade
        Task DeletarEspecialidade(string idAdm, string id);
    }
}
