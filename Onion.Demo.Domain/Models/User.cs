using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Onion.Demo.Domain.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();

        public string? PhoneNumber { get; private set; }

        public string? Name { get; private set; }

        public string UserName { get; private set; }

        public string? Email { get; private set; }

        public string Password { get; private set; }

        public string Salt { get; private set; }

        public bool ValidatePassword(string password) {

            return BCrypt.Net.BCrypt.HashPassword(password, Salt) == Password;
        }

        public static User Create(string userName, string password) {

            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            User user = new User()
            {
                //Id = Guid.NewGuid(),
                UserName = userName,
                Salt = salt,
                Password = BCrypt.Net.BCrypt.HashPassword(password, salt)
            };

            return user;
        }
    }
}
