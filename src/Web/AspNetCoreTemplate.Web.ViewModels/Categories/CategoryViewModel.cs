namespace AspNetCoreTemplate.Web.ViewModels.Categories
{
    using System.Collections.Generic;
    using System.Net;
    using System.Text.RegularExpressions;

    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ShortDescription
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Description, @"<[^>]+>", string.Empty));
                return content.Length > 300
                        ? content.Substring(0, 300) + "..."
                        : content;
            }
        }

        public string ImageUrl { get; set; }

        public IEnumerable<PostInCategoryViewModel> Posts { get; set; }
    }
}
