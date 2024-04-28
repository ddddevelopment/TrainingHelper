﻿namespace Auth.Domain.Models
{
    public class UserLogin
    {
        public UserLogin(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }
}
