using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommunityConnections.Models
{
    public partial class User
    {
        public User()
        {
            Alerts = new HashSet<Alert>();
        }

        [Display(Name = "Username")]
        public string UserName { get; set; } = null!;
        public string? Password { get; set; }
        public string? Email { get; set; }
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Authorization Question")]
        public string? AuthQuestion { get; set; }
        [Display(Name = "Authorization Answer")]
        public string? AuthAnswer { get; set; }

        public virtual ICollection<Alert> Alerts { get; set; }
    }
}
