using System;
using System.Collections.Generic;

namespace FriendsApp.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Gender {get;set;}
        public DateTime DateOfBirth { get; set; }
        
        // TODO: Change KnowAs to KnownAs;
        public string KnowAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        // For define a relationship needs to inverse what we have got in User class;
        public ICollection<Photo> Photos { get; set;}
    }
}