using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommunityConnections.Models
{
    public partial class Alert
    {
        [Display(Name = "Alert ID")]
        public int AlertId { get; set; }

        [Display(Name = "Time Posted")]
        public DateTime TimePosted { get; set; }

        [Display(Name = "Alert Type")]
        public string? AlertType { get; set; }

        [Display(Name = "Alert Title")]
        public string? AlertTitle { get; set; }

        [Display(Name = "Alert Description")]
        public string? AlertDescription { get; set; }

        [Display(Name = "ZIP Code")]
        public string? Zipcode { get; set; }

        public string? Location { get; set; }

        public string? Status { get; set; }

        public string? UserName { get; set; }
        [Display(Name = "Status")]
        public virtual Status? StatusNavigation { get; set; }
        [Display(Name = "Username")]
        public virtual User? UserNameNavigation { get; set; }

        public string TimePostedFormatted => TimePosted.ToString("MM/dd/yyyy hh:mm tt");
    }
}