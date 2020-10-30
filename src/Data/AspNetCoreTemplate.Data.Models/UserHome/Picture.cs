using System;
using System.ComponentModel.DataAnnotations;
using AspNetCoreTemplate.Data.Common.Models;

namespace AspNetCoreTemplate.Data.Models.UserHome
{
    public class Picture : BaseDeletableModel<string>
    {
        public Picture()
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
