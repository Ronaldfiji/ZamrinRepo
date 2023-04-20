using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeNext.Models
{
    public class AuthResult
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }

        public AuthResult() { }
    }
}
