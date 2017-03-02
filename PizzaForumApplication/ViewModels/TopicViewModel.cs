namespace PizzaForumApplication.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class TopicViewModel
    {
        public TopicViewModel()
        {
            this.Replies = new List<ReplyViewModel>();
        }

        public string TopicName { get; set; }

        public int TopicId { get; set; }

        public UserViewModel Author { get; set; }

        public DateTime PublishedOn { get; set; }

        public CategoryViewModel Category { get; set; }

        public List<ReplyViewModel> Replies { get; set; }
    }
}