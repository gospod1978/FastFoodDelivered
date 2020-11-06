namespace AspNetCoreTemplate.Services.Data.UserService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;

    public class ImagesService : IImagesService
    {
        private readonly IDeletableEntityRepository<Photo> imageRepository;

        public ImagesService(IDeletableEntityRepository<Photo> imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        public async Task<string> CreateAsyncImage(string name, string exstention, byte[] data, string userDataId)
        {
            var image = new Photo
            {
                Name = name,
                Exstention = exstention,
                DataFiles = data,
                UserDataId = userDataId,
            };

            await this.imageRepository.AddAsync(image);

            await this.imageRepository.SaveChangesAsync();

            return image.Id;
        }

        public async void DeleteById(string id)
        {
            var entity = this.imageRepository.All().Where(x => x.Id == id).FirstOrDefault();

            this.imageRepository.Delete(entity);
            await this.imageRepository.SaveChangesAsync();
        }

        public async void DeleteByName(string name)
        {
            var entity = this.imageRepository.All().Where(x => x.Name == name).FirstOrDefault();

            this.imageRepository.Delete(entity);
            await this.imageRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllImages<T>(int? count = null)
        {
            IQueryable<Photo> query =
               this.imageRepository.All().Where(x => x.IsDeleted == false).OrderByDescending(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllImagesByUser<T>(string id, int? count = null)
        {
            IQueryable<Photo> query =
               this.imageRepository.All().Where(x => x.UserDataId == id && x.IsDeleted == false).OrderByDescending(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllImagesByUserNoActive<T>(string id, int? count = null)
        {
            IQueryable<Photo> query =
              this.imageRepository.AllWithDeleted().Where(x => x.UserDataId == id && x.IsDeleted == true).OrderByDescending(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllImagesNoActive<T>(int? count = null)
        {
            IQueryable<Photo> query =
               this.imageRepository.AllWithDeleted().Where(x => x.IsDeleted == true).OrderByDescending(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var entity = this.imageRepository.All().Where(x => x.Id == id)
                 .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetByIdDelete<T>(string id)
        {
            var entity = this.imageRepository.AllWithDeleted().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetByName<T>(string name)
        {
            var entity = this.imageRepository.All().Where(x => x.Name == name)
                .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetByNameDelete<T>(string name)
        {
            var entity = this.imageRepository.AllWithDeleted().Where(x => x.Name == name)
                .To<T>().FirstOrDefault();

            return entity;
        }
    }
}
