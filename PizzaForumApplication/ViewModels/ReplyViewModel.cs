namespace PizzaForumApplication.ViewModels
{
    using System;

    public class ReplyViewModel
    {
        public UserViewModel User { get; set; }

        public DateTime PublishedOn { get; set; }

        public string Content { get; set; }

        public string ImgUrl { get; set; }
    }
}