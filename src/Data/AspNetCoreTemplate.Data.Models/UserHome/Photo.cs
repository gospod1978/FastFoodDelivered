namespace AspNetCoreTemplate.Data.Models.UserHome
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Common.Models;

    public class Photo : BaseDeletableModel<string>
    {
        public Photo()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string UserDataId { get; set; }

        public UserData UserData { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Exstention { get; set; }

        [MaxLength]
        public byte[] DataFiles { get; set; }
    }
}
