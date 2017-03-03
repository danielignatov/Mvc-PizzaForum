namespace PizzaForumApplication.ViewModels
{
    using System.Collections.Generic;

    public class ForumProfileViewModel
    {
        public ForumProfileViewModel()
        {
            this.Topics = new List<TopicViewModel>();
        }

        public int ProfileId { get; set; }

        public string ProfileName { get; set; }

        public NavbarViewModel Navbar { get; set; }

        public List<TopicViewModel> Topics { get; set; }
    }
}