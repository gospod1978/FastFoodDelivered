namespace AspNetCoreTemplate.Web.ViewModels.Users
{
    using System;
    using System.Net;
    using System.Text.RegularExpressions;

    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;

    public class EmailModel : IMapFrom<Email>
    {
        public string Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string BodyDes
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Body, @"<[^>]+>", string.Empty));
                return content;
            }
        }

        public string OriginalEmail { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ShortBody
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Body, @"<[^>]+>", string.Empty));
                return content.Length > 5
                        ? content.Substring(0, 5) + "..."
                        : content;
            }
        }
    }
}
