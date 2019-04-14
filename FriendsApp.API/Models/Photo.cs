using System;

namespace FriendsApp.API.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdd { get; set; }
        public bool IsMain { get; set; }

        // For define a relationship needs to inverse what we have got in User class;
        public User User { get; set; }

        // This property it's necessary for EF create an cascade delete instead create an restrict delete
            // (when an user is deleted, the photos will be deleted too).
        public int UserId { get; set; }
    }
}