namespace AspNetCoreTemplate.Web.ViewModels.UsersData
{
    using System;

    public class UserDocumentPdfDetail
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string FileType { get; set; }

        public byte[] DataFiles { get; set; }

        public DateTime CreatedOn { get; set; }

        public string RaelName { get; set; }
    }
}
