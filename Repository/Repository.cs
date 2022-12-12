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
        private static readonly string Endpoint = "https://db-vida.documents.azure.com:443/";
        private static readonly string Key = "CqzzcSBParBTTTcpgVLckyWOmpimIPNq6bLnlFTPg80CkyY8QqusQbNLmnt5WxVu9i1TGCQPRG2DACDbHy0Ilg=="; //C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==
        private static readonly string DatabaseId = "VidaBD";
        private static string CollectionId = "ContainerVidaBD";
        private static DocumentClient client;



        // TODO: listar, cdastrar, deletar e dar update em doctor, patient, schedulings..

        // -------------------------------------------- BDO -------------------------------------
        //Inicializa as collections especificadas (método chamado na classe Startup)
        public static void Initialize(string collectionId, string databaseId)
        {
            CollectionId = collectionId;
            client = new DocumentClient(new Uri(Endpoint), Key, new ConnectionPolicy { EnableEndpointDiscovery = false });
            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync(collectionId, databaseId).Wait();
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
        private static async Task CreateCollectionIfNotExistsAsync(string collectionId, string databaseId)
        {
            DocumentCollection myCollection = new DocumentCollection();
            myCollection.Id = collectionId;
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId));

                // Create collection with 400 RU/s

//              await client.CreateDocumentCollectionAsync(UriFactory.CreateDatabaseUri(DatabaseId), myCollection,
  //            new RequestOptions { OfferThroughput = 400 });
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Não encontrou collection!");
                    await client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(databaseId),
                        new DocumentCollection
                        {
                            Id = collectionId
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


        public static async Task DeleteItem(string id)
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

        // -------------------------------------------- ADMINISTRATOR -------------------------------------

        public static async Task<AdmEntity> GetAdm(string id, string collectionId)
        {
            try
            {
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, collectionId, id));
                return (AdmEntity)(dynamic)document;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<IEnumerable<AdmEntity>> ListAdm()
        {
            try
            {
                IDocumentQuery<AdmEntity> query = client.CreateDocumentQuery<AdmEntity>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                    new FeedOptions { MaxItemCount = -1 })
                    .AsDocumentQuery();

                List<AdmEntity> results = new List<AdmEntity>();
                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<AdmEntity>());
                }

                return results;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        // Busca um administrador especificao by seu email
        public static async Task<AdmEntity> GetAdmByEmail(string email)
        {
            try
            {
                AdmEntity adm = new AdmEntity();

                adm = client.CreateDocumentQuery<AdmEntity>(
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

        public static async Task<DoctorEntity> GetDoctorEntityByEmail2(AdmEntity adm, string email)
        {
                return null;

        }

        public static async Task<DoctorEntity> GetDoctorEntity(string idAdm, string id)
        {
                return null;

        }

        //Atualiza o adm a partir do Cadastro de um servico
        public static async Task<Document> RegisterDoctorEntity(AdmEntity adm, DoctorEntity prof)
        {
                return null;

        }

        //Atualiza a categoria a partir do Cadastro de Serviço
        public static async Task<Document> UpdateDoctorEntity(AdmEntity adm, DoctorEntity profissional)
        {
                return null;

        }

        public static async Task<Document> DeleteDoctorEntity(DoctorEntity prof, AdmEntity adm)
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

        public static async Task<IEnumerable<ExamEntity>> ListExams(string collectionId)
        {          
              try
            {
                IDocumentQuery<ExamEntity> query = client.CreateDocumentQuery<ExamEntity>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                    new FeedOptions { MaxItemCount = -1 })
                    .AsDocumentQuery();

                List<ExamEntity> results = new List<ExamEntity>();
                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<ExamEntity>());
                }

                return results;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<IEnumerable<ConsultEntity>> ListConsults(string collectionId)
        {          
              try
            {
                IDocumentQuery<ConsultEntity> query = client.CreateDocumentQuery<ConsultEntity>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                    new FeedOptions { MaxItemCount = -1 })
                    .AsDocumentQuery();

                List<ConsultEntity> results = new List<ConsultEntity>();
                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<ConsultEntity>());
                }

                return results;
            }
            catch (Exception e)
            {
                return null;
            }
        }



        // -------------------------------------------- PATIENT -------------------------------------
        // TODO: add collectionId, databaseId
        public static async Task<IEnumerable<PatientEntity>> ListPatients(string collectionId)
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


        public static async Task<PatientEntity> GetPatient(AdmEntity adm,string idPatient, string collectionId)
        {            
            try
            {
                PatientEntity pj = new PatientEntity();

                List<PatientEntity> results = new List<PatientEntity>();
                foreach (PatientEntity p in adm.Patient)
                {
                    results.Add(p);
                }
                PatientEntity pat = results.Where(x => x.UserId == idPatient).ToList()[0];
                return pat;
            }
            catch (Exception e)
            {
                return null;
            }
        }

// TODO: add patients no domain do adm + idAdm em patient
// usar como base:  https://bitbucket.org/atheninhas/athena-backend/src/master/Domain/Administrador.cs
        public static async Task<PatientEntity> GetPatientByCpf(AdmEntity adm, string cpf, string collectionId)
        {            
            try
            {
                PatientEntity pj = new PatientEntity();

                List<PatientEntity> results = new List<PatientEntity>();
                foreach (PatientEntity p in adm.Patient)
                {
                    results.Add(p);
                }
                PatientEntity pat = results.Where(x => x.Cpf == cpf).ToList()[0];
                return pat;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao buscar PATIENT por cpf!");
                return null;
            }
        }

        public static async Task<Document> RegisterExam(ExamEntity exam, string collectionId)
        {            
             try
            {
                return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), exam);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<Document> RegisterConsult(ConsultEntity consult, string collectionId)
        {            
             try
            {
                return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), consult);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //add novo patient naquele adm
        public static async Task<Document> RegisterPatient(PatientEntity pat, string collectionId)
        {            
             try
            {
                return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), pat);
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static async Task<Document> UpdatePatient(string idPatient, PatientEntity patient, string collectionId)
        {           

             try
            {
                return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, collectionId, idPatient), patient);
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static async Task<Document> DeletePatient(string idPatient, string collectionId)
        {            
             try
            {
               return await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, collectionId, idPatient));
            }
            catch (Exception e)
            {
                throw null;
            }

        }





        // -------------------------------------------- MEDICALCENTER -------------------------------------
        // lista os medicalcenters a partir de medicalinsurance
        public static async Task<IEnumerable<MedicalCenterEntity>> ListMedicalCenters(string idAdm, string collectionId)
        {          
              try
            {
                IDocumentQuery<MedicalCenterEntity> query = client.CreateDocumentQuery<MedicalCenterEntity>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, collectionId),
                    new FeedOptions { MaxItemCount = -1 })
                    .AsDocumentQuery();

                List<MedicalCenterEntity> results = new List<MedicalCenterEntity>();
                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<MedicalCenterEntity>());
                }

                return results;
            }
            
            catch (Exception e)
            {
                Console.WriteLine("erro AO listar MEDICALCENTERS: ", e.Message);
                return null;
            }
        }


        public static async Task<IEnumerable<MedicalCenterEntity>> ListMedicalCentersFromMedicalInsurance(string collectionId, string idMedicalInsurance)
        {          
              try
            {
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, collectionId, idMedicalInsurance));
                MedicalInsuranceEntity mi = (MedicalInsuranceEntity)(dynamic)document;

                List<MedicalCenterEntity> results = new List<MedicalCenterEntity>();
                foreach (MedicalCenterEntity p in mi.MedicalCenters)
                {
                    results.Add(p);
                }

//                return results.Where(x => x.IdMedicalInsurance == mi.EnterpriseId).ToList();
                  return results;
            }
            catch (Exception e)
            {
                Console.WriteLine("erro ao listar MEDICALCENTER a partir de MEDICALINSURANCE: ", e.Message);                
                return null;
            }
        }

        public static async Task<MedicalCenterEntity> GetMedicalCenter(string idMedicalCenter, string collectionId)
        {            
            try{
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, collectionId, idMedicalCenter));
                return (MedicalCenterEntity)(dynamic)document;
            }catch (Exception e){
                Console.WriteLine("erro ao PEGAR um medicalcenter: ", e.Message);
                return null;
            }
        }

        
        public static async Task<MedicalCenterEntity> GetMedicalCenterByName(string nome, string collectionId)
        {            
            try
            {
                MedicalCenterEntity mc = new MedicalCenterEntity();

                mc = client.CreateDocumentQuery<MedicalCenterEntity>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, collectionId),
                    new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                        .Where(x => x.CenterName == nome)
                        .AsEnumerable()
                        .FirstOrDefault();

                return mc;
            }
            catch (Exception e)
            {
                return null;
            }
        }



        public static async Task<Document> RegisterMedicalCenter(MedicalCenterEntity mc, string collectionId)
        {            
            try{
                return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, collectionId), mc);
            } catch (Exception e){
                Console.WriteLine("erro : ao add MEDICALCENTER", e.Message);
                return null;
            }

        }

// atualiza medical center dentro de medicalinsurance
      public static async Task<Document> UpdateMedicalCenter(MedicalCenterEntity mc, MedicalInsuranceEntity medicalInsurance, string collectionId)
        {          

            try
            {
                foreach (MedicalCenterEntity m in medicalInsurance.MedicalCenters)
                {
                    if (m.Id == mc.Id)
                    {
                        m.Id = mc.Id;
                        m.CenterAdress = mc.CenterAdress;
                        m.CenterName = mc.CenterName;
                        m.MedicalSpecialty = mc.MedicalSpecialty;
                        m.IdMedicalInsurance = mc.IdMedicalInsurance;
                        break;
                    }
                }
                return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, collectionId, medicalInsurance.EnterpriseId), medicalInsurance);
            }
            catch (Exception e)
            {
                return null;
            }
        }

//deletar medical center dentro de medicalinsurance
        public static async Task<Document> DeleteMedicalCenter(MedicalCenterEntity mc, MedicalInsuranceEntity medicalInsurance, string collectionId)
        {
            try
            {
                ICollection<MedicalCenterEntity> medicalCenters = new List<MedicalCenterEntity>(medicalInsurance.MedicalCenters);

                var item = medicalCenters.SingleOrDefault(x => x.Id == mc.Id);
                if (item != null)
                {
                    medicalCenters.Remove(item);
                    medicalInsurance.MedicalCenters = medicalCenters;
                }

                return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, collectionId, medicalInsurance.EnterpriseId), medicalInsurance);
            }
            catch (Exception e)
            {
                return null;
            }


        }




        // -------------------------------------------- MEDICALINSURANCE -------------------------------------
        // lista os medicalcenters a partir de medicalinsurance
        public static async Task<IEnumerable<MedicalInsuranceEntity>> ListMedicalInsurances(string idAdm, string collectionId)
        {          
              try
            {
                IDocumentQuery<MedicalInsuranceEntity> query = client.CreateDocumentQuery<MedicalInsuranceEntity>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, collectionId),
                    new FeedOptions { MaxItemCount = -1 })
                    .AsDocumentQuery();

                List<MedicalInsuranceEntity> results = new List<MedicalInsuranceEntity>();
                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<MedicalInsuranceEntity>());
                }

                return results;
            }
            
            catch (Exception e)
            {
                Console.WriteLine("erro AO listar MedicalInsurance: ", e.Message);
                return null;
            }
        }

        public static async Task<IEnumerable<MedicalInsuranceEntity>> ListAllMedicalInsurances(string collectionId)
        {          
              try
            {
                IDocumentQuery<MedicalInsuranceEntity> query = client.CreateDocumentQuery<MedicalInsuranceEntity>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, collectionId),
                    new FeedOptions { MaxItemCount = -1 })
                    .AsDocumentQuery();

                List<MedicalInsuranceEntity> results = new List<MedicalInsuranceEntity>();
                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<MedicalInsuranceEntity>());
                }

                return results;
            }
            
            catch (Exception e)
            {
                Console.WriteLine("erro AO listar MedicalInsurance: ", e.Message);
                return null;
            }
        }

        public static async Task<MedicalInsuranceEntity> GetMedicalInsurance(string idMedicalInsurance, string collectionId)
        {            
            try{
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, collectionId, idMedicalInsurance));
                return (MedicalInsuranceEntity)(dynamic)document;
            }catch (Exception e){
                Console.WriteLine("erro ao PEGAR um MedicalInsurance: ", e.Message);
                return null;
            }
        }

        public static async Task<MedicalInsuranceEntity> GetMedicalInsuranceByName(string nome, string collectionId)
        {            
            try
            {
                MedicalInsuranceEntity mi = new MedicalInsuranceEntity();

                mi = client.CreateDocumentQuery<MedicalInsuranceEntity>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, collectionId),
                    new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                        .Where(x => x.EnterpriseName == nome)
                        .AsEnumerable()
                        .FirstOrDefault();

                return mi;
            }
            catch (Exception e)
            {
                return null;
            }
        }



        public static async Task<Document> RegisterMedicalInsurance(MedicalInsuranceEntity mi, string collectionId)
        {            
            try{
                return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, collectionId), mi);
            } catch (Exception e){
                Console.WriteLine("erro : ao add MEDICALCENTER", e.Message);
                return null;
            }

        }

// atualiza medicalinsurance center dentro de medicalcenter
      public static async Task<Document> UpdateMedicalInsurance(/*ICollection<MedicalCenterEntity> medicalCenters,*/ MedicalInsuranceEntity mi, string collectionId)
        {          

            try
            {
                // TODO: verificar esta logica
/*                foreach(var mc in medicalCenters){
                    foreach (MedicalInsuranceEntity m in mc.IdMedicalInsurance)
                    {
                        if (m.EnterpriseId == mi.EnterpriseId)
                        {
                            m.EnterpriseId = mi.EnterpriseId;
                            m.EnterpriseName = mi.EnterpriseName;
                            m.MedicalCenters = mi.MedicalCenters;
                            m.MedicalPlans = mi.MedicalPlans;
                            break;
                        }

                    }

                }
                return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, collectionId, medicalCenter.Id), medicalCenter);*/
                                return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, collectionId, mi.EnterpriseId), mi);

            }
            catch (Exception e)
            {
                return null;
            }
        }

//deletar medical center dentro de medicalinsurance

        public static async Task<Document> DeleteMedicalInsurance(MedicalInsuranceEntity mi, ICollection<MedicalCenterEntity> medicalCenters, string collectionId)
        {
            try
            {

               foreach(var mc in medicalCenters){
                    foreach (MedicalInsuranceEntity m in mc.IdMedicalInsurance)
                    {
                        if(m.EnterpriseId == mi.EnterpriseId){
                            mc.IdMedicalInsurance = null;
                        }
                    }
               }

//                return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, mi.EnterpriseId), mi);
                return await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, collectionId, mi.EnterpriseId));

            }
            catch (Exception e)
            {
                return null;
            }


        }





        // -------------------------------------------- TOKEN -----------------------------------------
        public static async Task<AdministratorEntityDTO> GetEmailAsync(string email, string senha, string collectionId)
        {
            try
            {
                IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(DatabaseId, collectionId),
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

        public static async Task<AdministratorEntityDTO> GetPasswordAsync(string senha, string collectionId)
        {
            try
            {
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, collectionId, senha));
                return (AdministratorEntityDTO)(dynamic)document;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
