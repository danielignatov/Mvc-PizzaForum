namespace PizzaForumApplication.ViewModels
{
    using System.Collections.Generic;

    public class TopicsNewViewModel
    {
        public string Username { get; set; }

        public int UserId { get; set; }

        public int UserLevel { get; set; }

        public List<CategoryViewModel> Categories { get; set; }
    }
}