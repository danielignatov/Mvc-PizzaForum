namespace PizzaForumApplication.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Login
    {
        [Key]
        public int Id { get; set; }

        public virtual User User { get; set; }

        public bool IsActive { get; set; }

        public string SessionId { get; set; }
    }
}