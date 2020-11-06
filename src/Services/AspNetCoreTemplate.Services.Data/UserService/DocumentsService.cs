namespace AspNetCoreTemplate.Services.Data.UserService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;

    public class DocumentsService : IDocumentsService
    {
        private readonly IDeletableEntityRepository<Document> documentRepository;

        public DocumentsService(IDeletableEntityRepository<Document> documentRepository)
        {
            this.documentRepository = documentRepository;
        }

        public async Task<string> CreateAsyncDocument(string name, string fileType, byte[] data, string userDataId)
        {
            var document = new Document
            {
                Name = name,
                FileType = fileType,
                DataFiles = data,
                UserDataId = userDataId,
            };

            await this.documentRepository.AddAsync(document);

            await this.documentRepository.SaveChangesAsync();

            return document.Id;
        }

        public IEnumerable<T> GetAllDocuments<T>(int? count = null)
        {
            IQueryable<Document> query =
               this.documentRepository.All().Where(x => x.IsDeleted == false).OrderByDescending(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllDocumentsByUser<T>(string id, int? count = null)
        {
            IQueryable<Document> query =
               this.documentRepository.All().Where(x => x.UserDataId == id && x.IsDeleted == false).OrderByDescending(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var entity = this.documentRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetByName<T>(string name)
        {
            var entity = this.documentRepository.All().Where(x => x.Name == name)
                .To<T>().FirstOrDefault();

            return entity;
        }

        public async void DeleteById(string id)
        {
            var entity = this.documentRepository.All().Where(x => x.Id == id).FirstOrDefault();

            this.documentRepository.Delete(entity);
            await this.documentRepository.SaveChangesAsync();
        }

        public async void DeleteByName(string name)
        {
            var entity = this.documentRepository.All().Where(x => x.Name == name).FirstOrDefault();

            this.documentRepository.Delete(entity);
            await this.documentRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllDocumentsByUserNoActive<T>(string id, int? count = null)
        {
            IQueryable<Document> query =
              this.documentRepository.AllWithDeleted().Where(x => x.UserDataId == id && x.IsDeleted == true).OrderByDescending(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllDocumentsNoActive<T>(int? count = null)
        {
            IQueryable<Document> query =
               this.documentRepository.AllWithDeleted().Where(x => x.IsDeleted == true).OrderByDescending(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetByNameDelete<T>(string name)
        {
            var entity = this.documentRepository.AllWithDeleted().Where(x => x.Name == name)
                .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetByIdDelete<T>(string id)
        {
            var entity = this.documentRepository.AllWithDeleted().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return entity;
        }
    }
}
