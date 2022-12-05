using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Athenas.Controllers;
using Athenas.Domain;

namespace Athenas.Repository
{
    public class Repository<T> where T : class
    {
        private static readonly string Endpoint = "https://sn-athena-dev2.documents.azure.com:443/";
        private static readonly string Key = "3NR1lbh0SBxXgq2F64ZRRvlpXdUsXxrjnJJ4ZqOqQEG28gALXUxjjWBbcaeZU6PpUXcgWpeBTtu68m5rLsIm5w=="; //C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==
        private static readonly string DatabaseId = "Athena";
        private static string CollectionId = "Collection";
        private static DocumentClient client;

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

        public static async Task<IEnumerable<Administrator>> ListarAdm()
        {
            try
            {
                IDocumentQuery<Administrator> query = client.CreateDocumentQuery<Administrator>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                    new FeedOptions { MaxItemCount = -1 })
                    .AsDocumentQuery();

                List<Administrator> results = new List<Administrator>();
                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<Administrator>());
                }

                return results;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        // Busca um administrador especificao by seu email
        public static async Task<Administrator> GetAdmByEmail(string email)
        {
            try
            {
                Administrator adm = new Administrator();

                adm = client.CreateDocumentQuery<Administrator>(
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

        public static async Task<Document> AtualizarAdm(string id, T item)
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
        public static async Task<IEnumerable<DoctorEntity>> ListarDoctorEntity(string idAdm, string idServ)
        {
            try
            {
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, idAdm));
                Administrator adm = (Administrator)(dynamic)document;

                List<DoctorEntity> results = new List<DoctorEntity>();
                foreach (PessoaJuridica p in adm.PessoaJuridica)
                {
                    foreach (Categoria c in p.Categoria)
                    {
                        foreach (Servico s in c.Servico)
                        {
                            foreach (DoctorEntity pro in s.DoctorEntity)
                            {
                                results.Add(pro);
                            }
                        }
                    }
                }

                return results.Where(x => x.IdServico == idServ).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<DoctorEntity> GetDoctorEntityByEmail(string email)
        {
            try
            {
                DoctorEntity prof = new DoctorEntity();

                prof = client.CreateDocumentQuery<DoctorEntity>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                    new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                        .Where(x => x.Email == email)
                        .AsEnumerable()
                        .FirstOrDefault();

                return prof;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<DoctorEntity> GetDoctorEntityByEmail2(Administrator adm, string email)
        {
            try
            {
                List<DoctorEntity> results = new List<DoctorEntity>();
                foreach (PessoaJuridica pj in adm.PessoaJuridica)
                {
                    foreach (Categoria c in pj.Categoria)
                    {
                        foreach (Servico s in c.Servico)
                        {
                            foreach (DoctorEntity p in s.DoctorEntity)
                            {
                                results.Add(p);
                            }
                        }
                    }
                }
                DoctorEntity profissional = results.Where(x => x.Email == email).ToList()[0];
                return profissional;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<DoctorEntity> GetDoctorEntity(string idAdm, string id)
        {
            try
            {
                Administrator adm = await Repository<Administrator>.GetAdm(idAdm);

                List<DoctorEntity> results = new List<DoctorEntity>();
                foreach (PessoaJuridica pj in adm.PessoaJuridica)
                {
                    foreach (Categoria c in pj.Categoria)
                    {
                        foreach (Servico s in c.Servico)
                        {
                            foreach (DoctorEntity p in s.DoctorEntity)
                            {
                                results.Add(p);
                            }
                        }
                    }
                }
                DoctorEntity profissional = results.Where(x => x.Id == id).ToList()[0];
                return profissional;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //Atualiza o adm a partir do Cadastro de um servico
        public static async Task<Document> RegisterDoctorEntity(Administrator adm, DoctorEntity prof)
        {
            try
            {
                // recebe uma lista de pjs
                foreach (PessoaJuridica p in adm.PessoaJuridica)
                {
                    foreach (Categoria c in p.Categoria)
                    {
                        foreach (Servico s in c.Servico)
                        {
                            // verificar lista de categorias
                            if (s.DoctorEntity == null)
                            {
                                s.DoctorEntity = new List<DoctorEntity>();
                            }
                            if (s.Id == prof.IdServico)
                            {
                                s.DoctorEntity.Add(prof);
                            }
                        }
                    }
                }
                return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, adm.Id), adm);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //Atualiza a categoria a partir do Cadastro de Serviço
        public static async Task<Document> AtualizarDoctorEntity(Administrator adm, DoctorEntity profissional)
        {
            try
            {
                foreach (PessoaJuridica pj in adm.PessoaJuridica)
                {
                    foreach (Categoria c in pj.Categoria)
                    {
                        foreach (Servico s in c.Servico)
                        {
                            foreach (DoctorEntity p in s.DoctorEntity)
                            {
                                if (p.Id == profissional.Id)
                                {
                                    p.Id = profissional.Id;
                                    p.NameCompleto = profissional.NameCompleto;
                                    p.Email = profissional.Email;
                                    p.Pin = profissional.Pin;
                                    p.SchedulingEntity = profissional.SchedulingEntity;
                                    p.IdServico = profissional.IdServico;
                                    break;
                                }
                            }
                        }
                    }
                }
                return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, adm.Id), adm);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<Document> DeletarDoctorEntity(DoctorEntity prof, Administrator adm)
        {
            try
            {
                foreach (PessoaJuridica pj in adm.PessoaJuridica)
                {
                    foreach (Categoria c in pj.Categoria)
                    {
                        foreach (Servico s in c.Servico)
                        {
                            foreach (DoctorEntity p in s.DoctorEntity)
                            {
                                if (p.Id == prof.Id)
                                {
                                    ICollection<DoctorEntity> profs = new List<DoctorEntity>(s.DoctorEntity);

                                    var item = profs.SingleOrDefault(x => x.Id == prof.Id);
                                    if (item != null)
                                    {
                                        profs.Remove(item);
                                        s.DoctorEntity = profs;
                                    }
                                }
                            }
                        }
                    }
                }
                return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, adm.Id), adm);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        // -------------------------------------------- AGENDAMENTO -------------------------------------
        public static async Task<IEnumerable<SchedulingEntity>> ListarSchedulingEntitys(string idAdm, string idProf)
        {
            try
            {
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, idAdm));
                Administrator adm = (Administrator)(dynamic)document;

                List<SchedulingEntity> results = new List<SchedulingEntity>();
                foreach (PessoaJuridica p in adm.PessoaJuridica)
                {
                    foreach (Categoria c in p.Categoria)
                    {
                        foreach (Servico s in c.Servico)
                        {
                            foreach (DoctorEntity pro in s.DoctorEntity)
                            {
                                foreach (SchedulingEntity a in pro.SchedulingEntity)
                                {
                                    results.Add(a);
                                }
                            }
                        }
                    }
                }

                return results.Where(x => x.IdDoctorEntity == idProf).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<SchedulingEntity> GetSchedulingEntityPeloHorario(DateTime horario)
        {
            try
            {
                SchedulingEntity agend = new SchedulingEntity();

                agend = client.CreateDocumentQuery<SchedulingEntity>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                    new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                        .Where(x => x.Horario == horario)
                        .AsEnumerable()
                        .FirstOrDefault();

                return agend;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<SchedulingEntity> GetSchedulingEntityPeloHorario2(Administrator adm, DateTime horario)
        {
            try
            {
                List<SchedulingEntity> results = new List<SchedulingEntity>();
                foreach (PessoaJuridica pj in adm.PessoaJuridica)
                {
                    foreach (Categoria c in pj.Categoria)
                    {
                        foreach (Servico s in c.Servico)
                        {
                            foreach (DoctorEntity p in s.DoctorEntity)
                            {
                                foreach (SchedulingEntity a in p.SchedulingEntity)
                                {
                                    results.Add(a);
                                }
                            }
                        }
                    }
                }
                SchedulingEntity scheduling = results.Where(x => x.Horario == horario).ToList()[0];
                return scheduling;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<SchedulingEntity> GetSchedulingEntity(string idAdm, string id)
        {
            try
            {
                Administrator adm = await Repository<Administrator>.GetAdm(idAdm);
                List<SchedulingEntity> results = new List<SchedulingEntity>();
                foreach (PessoaJuridica pj in adm.PessoaJuridica)
                {
                    foreach (Categoria c in pj.Categoria)
                    {
                        foreach (Servico s in c.Servico)
                        {
                            foreach (DoctorEntity p in s.DoctorEntity)
                            {
                                foreach (SchedulingEntity a in p.SchedulingEntity)
                                {
                                    results.Add(a);
                                }
                            }
                        }
                    }
                }
                SchedulingEntity scheduling = results.Where(x => x.Id == id).ToList()[0];
                return scheduling;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //Atualiza o adm a partir do Cadastro de um servico
        public static async Task<Document> RegisterSchedulingEntity(Administrator adm, SchedulingEntity agend)
        {
            try
            {
                // recebe uma lista de pjs
                foreach (PessoaJuridica p in adm.PessoaJuridica)
                {
                    foreach (Categoria c in p.Categoria)
                    {
                        foreach (Servico s in c.Servico)
                        {
                            foreach (DoctorEntity pro in s.DoctorEntity)
                            {
                                // verificar lista de categorias
                                if (pro.SchedulingEntity == null)
                                {
                                    pro.SchedulingEntity = new List<SchedulingEntity>();
                                }
                                if (pro.Id == agend.IdDoctorEntity)
                                {
                                    pro.SchedulingEntity.Add(agend);
                                }
                            }
                        }
                    }
                }
                return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, adm.Id), adm);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<Document> AtualizarSchedulingEntity(Administrator adm, SchedulingEntity scheduling)
        {
            try
            {
                foreach (PessoaJuridica pj in adm.PessoaJuridica)
                {
                    foreach (Categoria c in pj.Categoria)
                    {
                        foreach (Servico s in c.Servico)
                        {
                            foreach (DoctorEntity p in s.DoctorEntity)
                            {
                                foreach (SchedulingEntity a in p.SchedulingEntity)
                                {
                                    if (a.Id == scheduling.Id)
                                    {
                                        a.Id = scheduling.Id;
                                        a.Dia = scheduling.Dia;
                                        a.Horario = scheduling.Horario;
                                        a.Cliente = scheduling.Cliente;
                                        a.IdDoctorEntity = scheduling.IdDoctorEntity;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, adm.Id), adm);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<Document> DeletarSchedulingEntity(SchedulingEntity agend, Administrator adm)
        {
            try
            {
                foreach (PessoaJuridica pj in adm.PessoaJuridica)
                {
                    foreach (Categoria c in pj.Categoria)
                    {
                        foreach (Servico s in c.Servico)
                        {
                            foreach (DoctorEntity p in s.DoctorEntity)
                            {
                                foreach (SchedulingEntity a in p.SchedulingEntity)
                                {
                                    if (a.Id == agend.Id)
                                    {
                                        ICollection<SchedulingEntity> agends = new List<SchedulingEntity>(p.SchedulingEntity);

                                        var item = agends.SingleOrDefault(x => x.Id == agend.Id);
                                        if (item != null)
                                        {
                                            agends.Remove(item);
                                            p.SchedulingEntity = agends;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, adm.Id), adm);
            }
            catch (Exception e)
            {
                return null;
            }
        }


        // -------------------------------------------- TOKEN -----------------------------------------
        public static async Task<Administrator> GetEmailAsync(string email, string senha)
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

                return (Administrator)(dynamic)results;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<Administrator> GetSenhaAsync(string senha)
        {
            try
            {
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, senha));
                return (Administrator)(dynamic)document;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
