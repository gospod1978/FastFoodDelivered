namespace AspNetCoreTemplate.Services.Data.UserService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;

    public class PictureService : IPictureService
    {
        private readonly IDeletableEntityRepository<Picture> pictureRepository;

        public PictureService(IDeletableEntityRepository<Picture> pictureRepository)
        {
            this.pictureRepository = pictureRepository;
        }

        public async Task<string> CreateAsyncImage(string name, string exstention, byte[] data, string orderId)
        {
            var image = new Picture
            {
                Name = name,
                Exstention = exstention,
                DataFiles = data,
                OrderId = orderId,
            };

            await this.pictureRepository.AddAsync(image);

            await this.pictureRepository.SaveChangesAsync();

            return image.Id;
        }

        public async void DeleteById(string id)
        {
            var entity = this.pictureRepository.All().Where(x => x.Id == id).FirstOrDefault();

            this.pictureRepository.Delete(entity);
            await this.pictureRepository.SaveChangesAsync();
        }

        public async void DeleteByName(string name)
        {
            var entity = this.pictureRepository.All().Where(x => x.Name == name).FirstOrDefault();

            this.pictureRepository.Delete(entity);
            await this.pictureRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllImages<T>(int? count = null)
        {
            IQueryable<Picture> query =
               this.pictureRepository.All().Where(x => x.IsDeleted == false).OrderByDescending(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllImagesByOrderId<T>(string id, int? count = null)
        {
            IQueryable<Picture> query =
               this.pictureRepository.All().Where(x => x.OrderId == id && x.IsDeleted == false).OrderByDescending(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllImagesByOrderNoActive<T>(string id, int? count = null)
        {
            IQueryable<Picture> query =
              this.pictureRepository.AllWithDeleted().Where(x => x.OrderId == id && x.IsDeleted == true).OrderByDescending(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllImagesNoActive<T>(int? count = null)
        {
            IQueryable<Picture> query =
               this.pictureRepository.AllWithDeleted().Where(x => x.IsDeleted == true).OrderByDescending(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var entity = this.pictureRepository.All().Where(x => x.Id == id)
                 .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetByIdDelete<T>(string id)
        {
            var entity = this.pictureRepository.AllWithDeleted().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetByName<T>(string name)
        {
            var entity = this.pictureRepository.All().Where(x => x.Name == name)
                .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetByNameDelete<T>(string name)
        {
            var entity = this.pictureRepository.AllWithDeleted().Where(x => x.Name == name)
                .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetByOrderId<T>(string orderId)
        {
            var entity = this.pictureRepository.All().Where(x => x.OrderId == orderId)
                .To<T>().FirstOrDefault();

            return entity;
        }
    }
}
