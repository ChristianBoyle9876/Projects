using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommunityConnections.Models
{
    public partial class Status
    {
        public Status()
        {
            Alerts = new HashSet<Alert>();
        }

        [Display(Name = "Status")]
        public string StatusName { get; set; } = null!;

        public virtual ICollection<Alert> Alerts { get; set; }
    }
}
