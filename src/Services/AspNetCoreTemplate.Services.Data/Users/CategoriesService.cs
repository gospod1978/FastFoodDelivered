namespace AspNetCoreTemplate.Services.Data.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<int> CreateAsync(string name, string title, string description, string imageUrl)
        {
            var category = new Category
            {
                Name = name,
                Title = title,
                Description = description,
                ImageUrl = imageUrl,
            };

            await this.categoryRepository.AddAsync(category);

            await this.categoryRepository.SaveChangesAsync();

            return category.Id;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Category> query =
                this.categoryRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var category = this.categoryRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return category;
        }

        public T GetByName<T>(string name)
        {
            var category = this.categoryRepository.All()
                .Where(x => x.Name.Replace(" ", "-") == name.Replace(" ", "-"))
                .To<T>().FirstOrDefault();

            return category;
        }
    }
}
