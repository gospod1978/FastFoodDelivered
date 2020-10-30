namespace AspNetCoreTemplate.Web.ViewModels.UsersData
{
    using System;

    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;

    public class DocumentDetails : IMapFrom<Document>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string FileType { get; set; }

        public byte[] DataFiles { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserDataId { get; set; }

        public UserData UserData { get; set; }
    }
}
