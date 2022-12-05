using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using ApiVida.Controllers;
using ApiVida.Domain;
using ApiVida.Domain.Entities;

namespace ApiVida.Repository
{
    public class Repository<T> where T : class
    {
        private static readonly string Endpoint = "https://db-vida.documents.azure.com/";
        private static readonly string Key = "CqzzcSBParBTTTcpgVLckyWOmpimIPNq6bLnlFTPg80CkyY8QqusQbNLmnt5WxVu9i1TGCQPRG2DACDbHy0Ilg=="; //C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==
        private static readonly string DatabaseId = "DBVida";
        private static string CollectionId = "ConteinerVida";
        private static DocumentClient client;


        // TODO: listar, cdastrar, deletar e dar update em doctor, patient, schedulings..

        // -------------------------------------------- BDO -------------------------------------
        //Inicializa as collections especificadas (método chamado na classe Startup)
        public static void Initialize(string collectionId)
        {
            CollectionId = collectionId;
            client = new DocumentClient(new Uri(Endpoint), Key, new ConnectionPolicy { EnableEndpointDiscovery = false });
            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }

        //Verifica se determinado banco de dados existe e se não exisitr o cria
        private static async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        //Verifica se uma collection existe e se não existir a cria
        private static async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(DatabaseId),
                        new DocumentCollection
                        {
                            Id = CollectionId
                        },
                        new RequestOptions { OfferThroughput = 400 });
                }
                else
                {
                    throw;
                }
            }
        }

        // -------------------------------------------- TODOS -------------------------------------

        public static async Task<Document> RegisterItem(T item)
        {
            try
            {
                return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), item);
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public static async Task DeletarItem(string id)
        {
            try
            {
                await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));
            }
            catch (Exception e)
            {
                throw null;
            }
        }

        // -------------------------------------------- ADMINISTRADOR -------------------------------------

        public static async Task<T> GetAdm(string id)
        {
            try
            {
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));
                return (T)(dynamic)document;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<IEnumerable<AdministratorEntityDTO>> ListAdm()
        {
            try
            {
                IDocumentQuery<AdministratorEntityDTO> query = client.CreateDocumentQuery<AdministratorEntityDTO>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                    new FeedOptions { MaxItemCount = -1 })
                    .AsDocumentQuery();

                List<AdministratorEntityDTO> results = new List<AdministratorEntityDTO>();
                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<AdministratorEntityDTO>());
                }

                return results;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        // Busca um administrador especificao by seu email
        public static async Task<AdministratorEntityDTO> GetAdmByEmail(string email)
        {
            try
            {
                AdministratorEntityDTO adm = new AdministratorEntityDTO();

                adm = client.CreateDocumentQuery<AdministratorEntityDTO>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                    new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                        .Where(x => x.Email == email)
                        .AsEnumerable()
                        .FirstOrDefault();
                return adm;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<Document> UpdateAdm(string id, T item)
        {
            try
            {
                return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id), item);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        // -------------------------------------------- DOCTOR -------------------------------------
        public static async Task<IEnumerable<DoctorEntity>> ListDoctorEntity(string idAdm, string idServ)
        {
                return null;

        }

        public static async Task<DoctorEntity> GetDoctorEntityByEmail(string email)
        {
                            return null;

        }

        public static async Task<DoctorEntity> GetDoctorEntityByEmail2(AdministratorEntityDTO adm, string email)
        {
                return null;

        }

        public static async Task<DoctorEntity> GetDoctorEntity(string idAdm, string id)
        {
                return null;

        }

        //Atualiza o adm a partir do Cadastro de um servico
        public static async Task<Document> RegisterDoctorEntity(AdministratorEntityDTO adm, DoctorEntity prof)
        {
                return null;

        }

        //Atualiza a categoria a partir do Cadastro de Serviço
        public static async Task<Document> UpdateDoctorEntity(AdministratorEntityDTO adm, DoctorEntity profissional)
        {
                return null;

        }

        public static async Task<Document> DeleteDoctorEntity(DoctorEntity prof, AdministratorEntityDTO adm)
        {
                return null;

        }
        // -------------------------------------------- SCHEDULING -------------------------------------
        public static async Task<IEnumerable<SchedulingEntity>> ListarSchedulings(string idAdm, string idProf)
        {            return null;
        }

        public static async Task<SchedulingEntity> GetSchedulingByHour(DateTime horario)
        {            return null;
        }

        public static async Task<SchedulingEntity> GetSchedulingByHour2(AdministratorEntityDTO adm, DateTime horario)
        {            return null;

        }

        public static async Task<SchedulingEntity> GetScheduling(string idAdm, string id)
        {
            return null;
        }

        //Atualiza o adm a partir do Cadastro de um servico
        public static async Task<Document> RegisterScheduling(AdministratorEntityDTO adm, SchedulingEntity agend)
        {            return null;

        }

        public static async Task<Document> UpdateScheduling(AdministratorEntityDTO adm, SchedulingEntity scheduling)
        {            return null;

        }

        public static async Task<Document> DeleteScheduling(SchedulingEntity agend, AdministratorEntityDTO adm)
        {            return null;

        }




        // -------------------------------------------- PATIENT -------------------------------------
        public static async Task<IEnumerable<PatientEntity>> ListPatients()
        {          
              try
            {
                IDocumentQuery<PatientEntity> query = client.CreateDocumentQuery<PatientEntity>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                    new FeedOptions { MaxItemCount = -1 })
                    .AsDocumentQuery();

                List<PatientEntity> results = new List<PatientEntity>();
                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<PatientEntity>());
                }

                return results;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<PatientEntity> GetPatient(string cpf)
        {            
            try{
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, cpf));
                return (PatientEntity)(dynamic)document;
            }catch (Exception e){
                return null;
            }
        }


        //Atualiza o adm a partir do Cadastro de um servico
        public static async Task<Document> RegisterPatient(PatientEntity pat)
        {            
            try{
                return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), pat);
            } catch (Exception e){
                return null;
            }

        }

        public static async Task<Document> UpdatePatient(AdministratorEntityDTO adm, SchedulingEntity scheduling)
        {            return null;

        }

        public static async Task<Document> DeletePatient(SchedulingEntity agend, AdministratorEntityDTO adm)
        {            return null;

        }




        // -------------------------------------------- TOKEN -----------------------------------------
        public static async Task<AdministratorEntityDTO> GetEmailAsync(string email, string senha)
        {
            try
            {
                IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                new FeedOptions { MaxItemCount = -1 })
                .AsDocumentQuery();

                List<T> results = new List<T>();
                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<T>());
                }

                return (AdministratorEntityDTO)(dynamic)results;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<AdministratorEntityDTO> GetPasswordAsync(string senha)
        {
            try
            {
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, senha));
                return (AdministratorEntityDTO)(dynamic)document;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
