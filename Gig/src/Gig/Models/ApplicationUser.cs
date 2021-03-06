﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace Gig.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> Followees { get; set; }
        public ICollection<UserNotification> Notification { get; set; }

        public ApplicationUser()
        {
            // Like following
            Followers = new Collection<Following>();
            // Like folllowers
            Followees = new Collection<Following>();
            Notification = new Collection<UserNotification>();
        }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Surname { get; set; }

        public string FullName
        {
            get
            {
                return String.Format("{0} {1}", FirstName, Surname);
            }
        }

        public void Notify(Notification notification)
        {
            Notification.Add(new UserNotification(Id, notification));
        }
    }
}
