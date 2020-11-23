namespace AspNetCoreTemplate.Data.Models.UserHome
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Common.Models;

    public class Email : BaseDeletableModel<string>
    {
        public Email()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public string OriginalEmail { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
