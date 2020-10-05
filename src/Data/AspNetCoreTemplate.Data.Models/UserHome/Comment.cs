namespace AspNetCoreTemplate.Data.Models.UserHome
{
    using AspNetCoreTemplate.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Content { get; set; }
    }
}
