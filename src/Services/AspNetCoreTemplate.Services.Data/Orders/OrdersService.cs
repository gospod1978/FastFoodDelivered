namespace AspNetCoreTemplate.Services.Data.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.Orders;
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;

    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Order> orderRepository;
        private readonly IDeletableEntityRepository<Picture> pictureRepository;

        public OrdersService(
            IDeletableEntityRepository<Order> orderRepository,
            IDeletableEntityRepository<Picture> pictureRepository)
        {
            this.orderRepository = orderRepository;
            this.pictureRepository = pictureRepository;
        }

        public async Task<string> CreateAsyncMenu(string name, string description, string restaurantId, string timePreparation, decimal price, int categoryId)
        {
            var order = new Order
            {
                Name = name,
                Description = description,
                RestaurantId = restaurantId,
                TimePrepartion = timePreparation,
                Price = price,
                PromotionType = PromotionType.NoPromotion,
                CategoryId = categoryId,
            };

            await this.orderRepository.AddAsync(order);
            await this.orderRepository.SaveChangesAsync();
            return order.Id;
        }

        public IEnumerable<T> GetAllByCategoryId<T>(int id, int? count = null)
        {
            IQueryable<Order> query =
              this.orderRepository.All().Where(a => a.CategoryId == id).OrderByDescending(x => x.Price);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByCategoryName<T>(string name, int? count = null)
        {
            IQueryable<Order> query =
              this.orderRepository.All().Where(a => a.Category.Name == name).OrderByDescending(x => x.Price);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByCourierId<T>(string id, int? count = null)
        {
            IQueryable<Order> query =
            this.orderRepository.All().Where(a => a.CourierId == id).OrderByDescending(x => x.Price);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByPrice<T>(decimal price, int? count = null)
        {
            IQueryable<Order> query =
            this.orderRepository.All().Where(a => a.Price <= price).OrderByDescending(x => x.Price);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByPromotionId<T>(string id, int? count = null)
        {
            var promotion = (PromotionType)Enum.Parse(typeof(PromotionType), id);
            IQueryable<Order> query =
            this.orderRepository.All().Where(a => a.PromotionType == promotion).OrderByDescending(x => x.Price);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByPromotionName<T>(string name, int? count = null)
        {
            var promotion = (PromotionType)Enum.Parse(typeof(PromotionType), name);
            IQueryable<Order> query =
            this.orderRepository.All().Where(a => a.PromotionType == promotion).OrderByDescending(x => x.Price);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByRestaurantId<T>(string id, int? count = null)
        {
            IQueryable<Order> query =
            this.orderRepository.All().Where(a => a.RestaurantId == id).OrderByDescending(x => x.Price);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByRestaurantName<T>(string name, int? count = null)
        {
            IQueryable<Order> query =
            this.orderRepository.All().Where(a => a.Restaurant.User.UserName == name).OrderByDescending(x => x.Price);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByUserId<T>(string id, int? count = null)
        {
            IQueryable<Order> query =
            this.orderRepository.All().Where(a => a.UserDataId == id).OrderByDescending(x => x.Price);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllOrders<T>(int? count = null)
        {
            IQueryable<Order> query =
            this.orderRepository.All().OrderByDescending(x => x.Price);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var entity = this.orderRepository.All().Where(x => x.Id == id)
                 .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetByName<T>(string name)
        {
            var entity = this.orderRepository.All().Where(x => x.Name == name)
                 .To<T>().FirstOrDefault();

            return entity;
        }

        public void AddImageToOrder(string orderId, string pictureId)
        {
            var entity = this.orderRepository.All().Where(x => x.Id == orderId).FirstOrDefault();
            entity.Picture = pictureId;
            this.orderRepository.Update(entity);
            this.orderRepository.SaveChangesAsync();
        }

        public string ChangePromotion(string orderID, string promotionId)
        {
            var entity = this.GetById<Order>(orderID);
            var newPromotiontype = (PromotionType)Enum.Parse(typeof(PromotionType), promotionId);
            entity.PromotionType = newPromotiontype;
            this.orderRepository.Update(entity);
            this.orderRepository.SaveChangesAsync();

            return entity.Id;
        }
    }
}
