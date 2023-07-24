using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace manualdojovem.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public int UserNameLimitChange { get; set; } = 20;

        public byte[] ProfilePicture { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
    }
}