using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeNext.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string MyImage { get; set; }

        public string Email { get; set; }
        public int age { get; set; }
        public DateTime DOB { get; set; }

        //[Required, MaxLength(20), EmailAddress]
        public string Username { get; set; }
       // [Required]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

    }
}