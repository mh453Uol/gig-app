using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.Models
{
    public class Following
    {
        public Following()
        {

        }

        public Following(string followerId, string followeeId)
        {
            this.FollowerId = followerId;
            this.FolloweeId = followeeId;
        }

        [Required]
        //A person who want to follow someone 
        public string FollowerId { get; set; }

        [ForeignKey("FollowerId")]
        public ApplicationUser Follower { get; set; }

        [Required]
        //A person who you want to follow
        public string FolloweeId { get; set; }

        [ForeignKey("FolloweeId")]
        public ApplicationUser Followee { get; set; }

        public bool IsDeleted { get; set; }

        public void Unfollow()
        {
            IsDeleted = true;
        }
    }
}