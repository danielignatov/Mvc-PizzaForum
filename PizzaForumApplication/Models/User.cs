namespace PizzaForumApplication.Models
{
    using Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public int Id { get; set; }

        [MinLength(3)]
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public UserRole Role { get; set; }

        public string AvatarUrl { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
    }
}