namespace AspNetCoreTemplate.Services.Data.UserService
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;

    public class UsersDataService : IUsersDataService
    {
        private readonly IDeletableEntityRepository<UserData> dataRepository;

        public UsersDataService(IDeletableEntityRepository<UserData> dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        public async Task<string> CreateAsyncUserData(string name, string userId)
        {
            var exsist = this.dataRepository.All().Where(x => x.UserId == userId).FirstOrDefault();

            var id = string.Empty;

            if (exsist == null)
            {
                var data = new UserData
                {
                    Name = name,
                    UserId = userId,
                };
                await this.dataRepository.AddAsync(data);
                id = data.Id;
            }
            else
            {
                exsist.Name = name;
                this.dataRepository.Update(exsist);
                id = exsist.Id;
            }

            await this.dataRepository.SaveChangesAsync();

            return id;
        }

        public T GetByUserId<T>(string id)
        {
            var entity = this.dataRepository.All().Where(x => x.UserId == id)
               .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetByUserName<T>(string name)
        {
            var entity = this.dataRepository.All().Where(x => x.User.UserName == name)
               .To<T>().FirstOrDefault();

            return entity;
        }

        public string GetName(string id)
        {
            var entity = this.dataRepository.All().Where(x => x.UserId == id)
               .FirstOrDefault();

            return entity.Name;
        }

        public string GetId(string id)
        {
            var entity = this.dataRepository.All().Where(x => x.UserId == id)
               .FirstOrDefault();

            return entity.Id;
        }

        public string GetByDataId(string id)
        {
            var entity = this.dataRepository.All().Where(x => x.Id == id)
               .FirstOrDefault();

            return entity.UserId;
        }
    }
}
