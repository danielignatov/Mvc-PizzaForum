namespace PizzaForumApplication.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Reply
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        public string ImgUrl { get; set; }

        public DateTime PublishDate { get; set; }

        public virtual User Author { get; set; }
    }
}